using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Core.Helpers
{
    public class RequestHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestHandler(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public string GetHeaderValue(string headerKey)
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(headerKey))
            {
                throw new KeyNotFoundException($"Header key {headerKey} missing.");
            }

            return _httpContextAccessor.HttpContext.Request.Headers[headerKey].ToString();
        }
    }
}
