using Platform;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("{first:int}/{second:bool}", async context =>
//{
//    await context.Response.WriteAsync("Request Was Routed\n");
//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});

app.MapGet("{firs:alpha:length(3)}/{secon:bool}", async context =>
{
    await context.Response.WriteAsync("Request Was Routed\n");
    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

//app.MapGet("capital/{country=France}", Capital.Endpoint);
app.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint2).WithMetadata(new RouteNameMetadata("population2"));

//app.MapFallback(async context =>
//{
//    await context.Response.WriteAsync("Routed to fallback endpoint\n");

//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});

app.MapGet("{*smth}", async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint\n");

    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.Run();
