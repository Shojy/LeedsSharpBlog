using LeedsSharpBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LeedsSharpBlog.Middleware
{
    public class GeneratedByMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _blogTitle;

        public GeneratedByMiddleware(RequestDelegate next, IOptions<AppSettings> settings)
        {
            _next = next;
            _blogTitle = settings.Value.BlogTitle;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Generated-By", _blogTitle);
            await _next.Invoke(context);
        }
    }
}
