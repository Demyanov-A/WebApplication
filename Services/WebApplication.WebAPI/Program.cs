using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebApplication.DAL.Context;
using WebApplication.Domain.Entities.Identity;
using WebApplication.Interfaces.Services;
using WebApplication.Logging;
using WebApplication.Services.Services;
using WebApplication.Services.Services.InSQL;
using WebApplication.WebAPI.Infrastructure.Middleware;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    const string WebApplication_api_xml = "WebApplication.WebAPI.xml";
    const string WebApplication_domain_xml = "WebApplication.Domain.xml";
    const string debug_path = "bin/Debug/net6.0";

    if (File.Exists(WebApplication_api_xml))
        opt.IncludeXmlComments(WebApplication_api_xml);
    else if (File.Exists(Path.Combine(debug_path, WebApplication_api_xml)))
        opt.IncludeXmlComments(Path.Combine(debug_path, WebApplication_api_xml));

    if (File.Exists(WebApplication_domain_xml))
        opt.IncludeXmlComments(WebApplication_domain_xml);
    else if (File.Exists(Path.Combine(debug_path, WebApplication_domain_xml)))
        opt.IncludeXmlComments(Path.Combine(debug_path, WebApplication_domain_xml));
});

var database_type = builder.Configuration["Database"];
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

services.AddTransient<IDbInitializer, DbInitializer>();

services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebApplicationDB>()
    .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opt =>
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
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.AddScoped<IEmployeesData, SqlEmployeesData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IOrderService, SqlOrderService>();
//services.AddScoped<ICartService, InCookiesCartService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync(RemoveBefore: false).ConfigureAwait(true);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
