using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Services;

public class GuidService : IResponseFormatter
{
    private Guid guid = Guid.NewGuid();

    public GuidService()
    {

    }

    public async Task Format(HttpContext context, string content)
    {
        await context.Response.WriteAsync($"Guid: {guid}\n{content}");
    }
}