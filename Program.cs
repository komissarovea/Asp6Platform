using Platform;
using Microsoft.AspNetCore.HttpLogging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.Services.AddHttpLogging(opts =>
// {
//     opts.LoggingFields = HttpLoggingFields.RequestMethod
//         | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseStatusCode;
// });

WebApplication app = builder.Build();

app.UseHttpLogging();

app.UseStaticFiles();

app.UseMiddleware<QueryStringMiddleWare>();

app.MapGet("population/{city?}", Population.Endpoint);

app.Run();