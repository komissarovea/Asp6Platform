using Platform.Services;

namespace Platform;

public class WeatherEndpoint
{
    private IResponseFormatter formatter;
    public WeatherEndpoint(IResponseFormatter responseFormatter)
    {
        formatter = responseFormatter;
    }

    public async Task Endpoint(HttpContext context)
    {
        await formatter.Format(context, "Endpoint Class: It is cloudy in Milan");
    }

    //public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
    //{
    //    //IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
    //    await formatter.Format(context, "Endpoint CLASS3: It is cloudy in Milan");
    //}
}
