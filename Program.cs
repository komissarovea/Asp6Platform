using Microsoft.AspNetCore.HostFiltering;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDistributedMemoryCache(opts =>
// {
//     opts.SizeLimit = 200;
// });

builder.Services.AddDistributedSqlServerCache(opts =>
{
    opts.ConnectionString = builder.Configuration["ConnectionStrings:CacheConnection"];
    opts.SchemaName = "dbo";
    opts.TableName = "DataCache";
});

WebApplication app = builder.Build();

app.MapEndpoint<Platform.SumEndpoint>("/sum/{count:int=1000000000}");

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.Run();