using Platform;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(opts =>
{
    opts.ConstraintMap.Add("countryName",  typeof(CountryRouteConstraint));
});

var app = builder.Build();

//app.MapGet("{firs:alpha:length(3)}/{secon:bool}", async context =>
//{
//    await context.Response.WriteAsync("Request Was Routed\n");
//    foreach (var kvp in context.Request.RouteValues)
//    {
//        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
//    }
//});

app.MapGet("capital/{country:countryName}", Capital.Endpoint);
//app.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
app.MapGet("size/{city?}", Population.Endpoint2).WithMetadata(new RouteNameMetadata("population2"));


app.MapGet("{*path}", async context =>
{
    await context.Response.WriteAsync("Routed to fallback endpoint\n");

    foreach (var kvp in context.Request.RouteValues)
    {
        await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
    }
});

app.Run();
