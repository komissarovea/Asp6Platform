using Platform;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

var logger = app.Services
    .GetRequiredService<ILoggerFactory>().CreateLogger("Pipeline");

logger.LogDebug("Pipeline configuration starting");

app.MapGet("population/{city?}", Population.Endpoint);

logger.LogDebug(new EventId(77, "my_id"), "Pipeline configuration complete");

app.Run();