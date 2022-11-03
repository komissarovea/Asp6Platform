using Platform;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("files/{filename}.{ext}", async context =>
//{
//    await context.Response.WriteAsync("Request Was Routed\n");
//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});

app.MapGet("{first}/{second}/{catchall}", async context =>
{
    await context.Response.WriteAsync("Request Was Routed\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response
        .WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.MapGet("capital/{country=France}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint2).WithMetadata(new RouteNameMetadata("population2"));

app.MapGet(String.Empty, async (HttpContext context) =>
{
    await context.Response.WriteAsync("Default \n");
});

app.Run();
