var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddCommandLine(args);

var app = builder.Build();

//var configuration = app.Configuration;
//var greetings = configuration["CustomGreetings"];

app.MapGet("/", () => app.Configuration["CustomGreetings"]);

app.Run();
