WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error.html");
    app.UseStaticFiles();
}

app.UseStatusCodePages("text/html", Platform.Responses.DefaultResponse);

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/error")
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await Task.CompletedTask;
    }
    else
    {
        await next();
    }
});

// app.Run(context =>
// {

//     //throw new Exception("Something has gone wrong");
// });

app.Run();