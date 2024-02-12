using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.Extensions
{
    public class RequestResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly List<string> AllRequestHeaders = new List<string>();
        public readonly List<string> AllResponseHeaders = new List<string>();
        public RequestResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var requestHeaders = httpContext.Request.Headers.Where(x => AllRequestHeaders.All(h => h != x.Key)).Select(x => x.Key);
            AllRequestHeaders.AddRange(requestHeaders);
            await this._next.Invoke(httpContext);
            var uniqueResponseHeaders = httpContext.Response.Headers.Where(x => AllResponseHeaders.All(h => h != x.Key)).Select(x => x.Key);
            AllResponseHeaders.AddRange(uniqueResponseHeaders);
            await _next.Invoke(httpContext);
        }
    }
}
