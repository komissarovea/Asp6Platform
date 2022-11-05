using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IResponseFormatter, TextResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, HtmlResponseFormatter>();
builder.Services.AddScoped<IResponseFormatter, GuidService>();

WebApplication app = builder.Build();

app.MapGet("single", async context =>
{
    IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
    await formatter.Format(context, "Single service");
});
app.MapGet("/", async context =>
{
    IResponseFormatter formatter = context.RequestServices.GetServices<IResponseFormatter>().First(f => f.RichOutput);
    await formatter.Format(context, "Multiple services");
});

app.Run();