using Platform;
using Microsoft.AspNetCore.HttpLogging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = HttpLoggingFields.RequestMethod
        | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode;
});

WebApplication app = builder.Build();

// var logger = app.Services
//     .GetRequiredService<ILoggerFactory>().CreateLogger("Pipeline");

// logger.LogDebug("Pipeline configuration starting");

app.MapGet("population/{city?}", Population.Endpoint);

// logger.LogDebug(new EventId(77, "my_id"), "Pipeline configuration complete");

app.Run();