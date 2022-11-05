using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var servicesConfig = builder.Configuration;
builder.Services.Configure<MessageOptions>(servicesConfig.GetSection("Location"));

var servicesEnv = builder.Environment;
// - use environment to set up services

WebApplication app = builder.Build();

var pipelineConfig = app.Configuration;
// - use configuration settings to set up pipeline

var pipelineEnv = app.Environment;
// - use envirionment to set up pipeline

app.UseMiddleware<LocationMiddleware>();

app.MapGet("config", async (HttpContext context,
IConfiguration config, IWebHostEnvironment env) =>
{
    string defaultDebug = config["Logging:LogLevel:Default"];
    await context.Response.WriteAsync($"The config setting is: {defaultDebug}");
    await context.Response.WriteAsync($"\nThe env setting is: {env.EnvironmentName}");
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.Run();