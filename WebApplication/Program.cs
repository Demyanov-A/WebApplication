using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication.Infrastructure.Conventions;
using WebApplication.Infrastructure.Middleware;
using WebApplication.Services;
using WebApplication.Services.Interfaces;
using WebApplication.DAL.Context;
using Microsoft.Extensions.Configuration;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Services.InCookies;
using WebApplication.Services.InSQL;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

#region Настройка построителя приложения - определение содержимого

var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebApplicationDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocalDB")));

services.AddTransient<IDbInitializer, DbInitializer>();

//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

services.AddScoped<IEmployeesData, SqlEmployeesData>();

//services.AddSingleton<IProductData, InMemoryProductData>();

services.AddScoped<IProductData, SqlProductData>();

services.AddScoped<IOrderService, SqlOrderService>();

services.AddScoped<ICartService, InCookiesCartService>();

services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebApplicationDB>()
    .AddDefaultTokenProviders();

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
