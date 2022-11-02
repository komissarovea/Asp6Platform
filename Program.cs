using Microsoft.Extensions.Options;
using Platform;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName = "Albany";
});

var app = builder.Build();

//app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
//{
//    Platform.MessageOptions opts = msgOpts.Value;
//    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
//});

app.UseMiddleware<LocationMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
