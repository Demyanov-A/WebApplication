var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

//builder.Configuration.AddCommandLine(args);

#region Настройка построителя приложения - определение содержимого

var services = builder.Services;

services.AddControllersWithViews();

#endregion

//Сборка приложения
var app = builder.Build();

#region Конфигурирование конвейера обработки входящих соединений

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

//var configuration = app.Configuration;
//var greetings = configuration["CustomGreetings"];
//app.MapGet("/", () => app.Configuration["CustomGreetings"]);

app.MapGet("/throw", () =>
{
    throw new  ApplicationException("Ошибка в программе!");
});

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion

//Запуск приложения
app.Run();
