using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication.Infrastructure.Conventions;
using WebApplication.Infrastructure.Middleware;
using WebApplication.Services;
using WebApplication.DAL.Context;
using Microsoft.Extensions.Configuration;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Interfaces.Services;
using WebApplication.Interfaces.TestAPI;
using WebApplication.Logging;
using WebApplication.Services.Services;
using WebApplication.Services.Services.InCookies;
using WebApplication.Services.Services.InSQL;
using WebApplication.WebAPI.Clients.Employees;
using WebApplication.WebAPI.Clients.Identity;
using WebApplication.WebAPI.Clients.Orders;
using WebApplication.WebAPI.Clients.Products;
using WebApplication.WebAPI.Clients.Values;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();

#region Настройка построителя приложения - определение содержимого

var configuration = builder.Configuration;

var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

var database_type = builder.Configuration["DataBase"];

switch (database_type)
{
    default: throw new InvalidOperationException($"DataBase type {database_type} not supported");
    case "SqLite":
        services.AddDbContext<WebApplicationDB>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("SqLite"),
                o => o.MigrationsAssembly("WebApplication.DAL.SqLite")));
        break;
    case "SqlServer":
        services.AddDbContext<WebApplicationDB>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        break;
    case "SqlServerLocalDB":
        services.AddDbContext<WebApplicationDB>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocalDB")));
        break;
}

services.AddDbContext<WebApplicationDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocalDB")));

services.AddTransient<IDbInitializer, DbInitializer>();

//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

//services.AddScoped<IEmployeesData, SqlEmployeesData>();

//services.AddSingleton<IProductData, InMemoryProductData>();

//services.AddScoped<IProductData, SqlProductData>();

//services.AddScoped<IOrderService, SqlOrderService>();

services.AddScoped<ICartService, InCookiesCartService>();


//services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

//services.AddHttpClient<IEmployeesData, EmployeesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

//services.AddHttpClient<IProductData, ProductsClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

//services.AddHttpClient<IOrderService, OrdersClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

services.AddHttpClient("WebApplicationAPI", client => client.BaseAddress = new(configuration["WebAPI"]))
    .AddTypedClient<IValuesService, ValuesClient>()
    .AddTypedClient<IEmployeesData, EmployeesClient>()
    .AddTypedClient<IProductData, ProductsClient>()
    .AddTypedClient<IOrderService, OrdersClient>();

services.AddIdentity<User, Role>()
    //.AddEntityFrameworkStores<WebApplicationDB>()
    .AddDefaultTokenProviders();

services.AddHttpClient("WebApplicationAPIIdentity", client => client.BaseAddress = new(configuration["WebAPI"]))
    .AddTypedClient<IUserStore<User>, UsersClient>()
    .AddTypedClient<IUserRoleStore<User>, UsersClient>()
    .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
    .AddTypedClient<IUserEmailStore<User>, UsersClient>()
    .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
    .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
    .AddTypedClient<IUserClaimStore<User>, UsersClient>()
    .AddTypedClient<IUserLoginStore<User>, UsersClient>()
    .AddTypedClient<IRoleStore<Role>, RolesClient>();

services.AddAutoMapper(Assembly.GetEntryAssembly());

services.Configure<IdentityOptions>(opt=>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebApplication.GB";
    opt.Cookie.HttpOnly = true;
    //opt.Cookie.Expiration = TimeSpan.FromDays(10);
    opt.ExpireTimeSpan = TimeSpan.FromDays(10);
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.SlidingExpiration = true;

});

#endregion

var app = builder.Build(); //Сборка приложения

await using(var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

    await db_initializer.InitializeAsync(RemoveBefore: false).ConfigureAwait(true);
}

#region Конфигурирование конвейера обработки входящих соединений

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<TestMiddleware>();

app.UseWelcomePage("/welcome");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#endregion

app.Run();//Запуск приложения
