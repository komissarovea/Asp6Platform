using Platform;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("{first}/{second}/{third}", async context =>
{
    await context.Response.WriteAsync("Request Was Routed 3\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response .WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("{first}/{second}", async context =>
{
    await context.Response.WriteAsync("Request Was Routed 2\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("capital/{country}", new Capital().Invoke);
app.MapGet("population/{city}", Population.Endpoint);

app.MapGet(String.Empty, async (HttpContext context) =>
{
    await context.Response.WriteAsync("Default \n");
});

app.Run();
