using Platform;
using Platform.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

app.MapGet("/", async context => {
    await context.Response.WriteAsync("Hello World!");
});

app.Run();