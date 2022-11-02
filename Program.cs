using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get
        && context.Request.Query["custom"] == "true")
    {
        //HttpRequest httpRequest = context.Request;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next.Invoke();
});

app.MapGet("/", async (context) =>
{
    //context.Response.StatusCode = 202;
    await context.Response.WriteAsync("MapGet \n");
});

app.Run();
