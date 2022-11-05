using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
var servicesConfig = builder.Configuration;
builder.Services.Configure<MessageOptions>(servicesConfig.GetSection("Location"));

WebApplication app = builder.Build();

var pipelineConfig = app.Configuration;
// - use configuration settings to set up pipeline

app.UseMiddleware<LocationMiddleware>();

app.MapGet("config", async (HttpContext context, IConfiguration config) =>
{
    string defaultDebug = config["Logging:LogLevel:Default"];
    await context.Response.WriteAsync($"The config setting is: {defaultDebug}");

    string environ = config["ASPNETCORE_ENVIRONMENT"];
    await context.Response.WriteAsync($"\nThe env setting is: {environ}");
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.Run();