using ScripturesApi;

var builder = WebApplication.CreateBuilder(args); 

builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
});

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var app = builder.Build();

startup.Configure(app, builder.Environment);

app.Run();
