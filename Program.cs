using Platform;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// http://localhost:5000/branch?custom=true
app.Map("/branch", branch =>
{
    //branch.Run(async (HttpContext context) =>
    //{
    //    await context.Response.WriteAsync($"Branch F Middleware");
    //});
    branch.Run(new QueryStringMiddleWare().Invoke);
    branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"After Middleware");
    });
});

// http://localhost:5000/?branch2&custom=true
//app.MapWhen(context => context.Request.Query.Keys.Contains("branch2"), branch =>
//{
//    branch.UseMiddleware<QueryStringMiddleWare>();
//    branch.Use(async (HttpContext context, Func<Task> next) =>
//    {
//        await context.Response.WriteAsync($"Branch2 Middleware");
//    });
//});

//app.UseMiddleware<QueryStringMiddleWare>();

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync("MapGet \n");
});

app.Run();
