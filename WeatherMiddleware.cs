using Platform.Services;

namespace Platform
{
    public class WeatherMiddleware
    {
        private RequestDelegate next;
        // private IResponseFormatter formatter;

        public WeatherMiddleware(RequestDelegate nextDelegate, IResponseFormatter respFormatter)
        {
            next = nextDelegate;
            //formatter = respFormatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter)
        {
            if (context.Request.Path == "/middleware/class")
            {
                await formatter.Format(context, "Middleware CLASS: It is raining in London 2");
            }
            else
            {
                await next(context);
            }
        }
    }
}
