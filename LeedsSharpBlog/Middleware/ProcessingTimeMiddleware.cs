using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LeedsSharpBlog.Middleware
{
    public class ProcessingTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ProcessingTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            // Add the header at the last possible moment before all headers are sent to the client.
            // After this is too late, and response body will start.
            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                // Stopping the timer is required here to count the elapsed time
                watch.Stop();

                // Add the header.
                httpContext.Response.Headers.Add("X-Processing-Time-Milliseconds", new[] { watch.ElapsedMilliseconds.ToString() });
                return Task.FromResult(0);
            }, context);

            //Continue down the pipeline, the handler above will trigger when required.
            await _next(context);
        }
    }
}
