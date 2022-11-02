using Platform;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/short")
    {
        await context.Response.WriteAsync($"Request Short Circuited");
    }
    else
    {
        await next();
    }
});

app.Use(async (HttpContext context, Func<Task> next) =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
    // await context.Response.WriteAsync("Custom 2 \n");
});

app.UseMiddleware<QueryStringMiddleWare>();

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync("MapGet \n");
});

app.Run();
