using Platform.Services;

namespace Platform;

public class WeatherEndpoint
{
    public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
    {
        //IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
        await formatter.Format(context, "Endpoint CLASS3: It is cloudy in Milan");
    }
}
