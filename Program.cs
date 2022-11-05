using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager c1 = builder.Configuration;
IWebHostEnvironment e1 = builder.Environment;

IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
    builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();
}
else
{
    builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
}

builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

WebApplication app = builder.Build();
IConfiguration c2 = app.Configuration;
bool tmp = c1 == c2;
IWebHostEnvironment e2 = app.Environment;
tmp = e1 == e2;

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