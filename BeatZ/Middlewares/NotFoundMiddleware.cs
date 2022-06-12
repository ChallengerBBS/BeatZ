namespace BeatZ.Api.Middlewares
{
    public class NotFoundMiddleware: IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 404;
            }

            return Task.CompletedTask;
        }
    }
}
