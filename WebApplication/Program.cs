using WebApplication.Infrastructure.Conventions;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

#region Настройка построителя приложения - определение содержимого

var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

#endregion

var app = builder.Build(); //Сборка приложения

#region Конфигурирование конвейера обработки входящих соединений

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Home}/{action=Index}/{id?}");
#endregion

app.Run();//Запуск приложения
