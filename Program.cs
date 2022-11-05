using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//IWebHostEnvironment env = builder.Environment;
IConfiguration config = builder.Configuration;

builder.Services.AddScoped<IResponseFormatter>(serviceProvider => {
    string? typeName = config["services:IResponseFormatter"];
    return (IResponseFormatter)ActivatorUtilities .CreateInstance(serviceProvider, typeName == null
        ? typeof(GuidService) : Type.GetType(typeName, true)!);
});
builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

WebApplication app = builder.Build();

app.UseMiddleware<WeatherMiddleware>();

app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
});

//app.MapGet("endpoint/class", WeatherEndpoint.Endpoint2);
app.MapEndpoint<WeatherEndpoint>("endpoint/class");

app.MapGet("endpoint/function", async (HttpContext context) =>
{
    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
    await formatter.Format(context, "Endpoint Function: It is sunny in LA 2");
});

app.Run();