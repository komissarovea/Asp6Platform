using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IResponseFormatter, HtmlResponseFormatter>();
//builder.Services.AddScoped<IResponseFormatter, GuidService>();

builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

WebApplication app = builder.Build();
//var newScope = app.Services.CreateScope();

app.UseMiddleware<WeatherMiddleware>();

app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Middleware Function: It is snowing in Chicago");
});

//app.MapGet("endpoint/class", WeatherEndpoint.Endpoint2);
app.MapEndpoint<WeatherEndpoint>("endpoint/class");

//app.MapGet("endpoint/function", async (HttpContext context, IResponseFormatter formatter) =>
//{
//    await formatter.Format(context, "Endpoint Function: It is sunny in LA");
//});

app.MapGet("endpoint/function", async (HttpContext context) =>
{
    IResponseFormatter formatter1 = app.Services.CreateScope().ServiceProvider.GetRequiredService<IResponseFormatter>();
    IResponseFormatter formatter2 = context.RequestServices.GetRequiredService<IResponseFormatter>();
    bool tmp = formatter1 == formatter2;
    Console.WriteLine(tmp);
    await formatter1.Format(context, "Endpoint Function: It is sunny in LA 2");
});

app.Run();