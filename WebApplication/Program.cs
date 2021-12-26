using Microsoft.EntityFrameworkCore;
using WebApplication.Infrastructure.Conventions;
using WebApplication.Infrastructure.Middleware;
using WebApplication.Services;
using WebApplication.Services.Interfaces;
using WebApplication.DAL.Context;
using Microsoft.Extensions.Configuration;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

#region Настройка построителя приложения - определение содержимого

var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebApplicationDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

services.AddTransient<IDbInitializer, DbInitializer>();  

services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
services.AddSingleton<IProductData, InMemoryProductData>();

#endregion

var app = builder.Build(); //Сборка приложения

await using(var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

    await db_initializer.InitializeAsync(RemoveBefore: false;
}

#region Конфигурирование конвейера обработки входящих соединений

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));


app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TestMiddleware>();

app.UseWelcomePage("/welcome");

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion

app.Run();//Запуск приложения
