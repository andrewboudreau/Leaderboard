using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

// Resources
// http://www.asp.net/web-api/overview/working-with-http/http-message-handlers
// http://www.hanselman.com/blog/HTTPPUTOrDELETENotAllowedUseXHTTPMethodOverrideForYourRESTServiceWithASPNETWebAPI.aspx
    
namespace Leaderboard.Web.DelegatingHandlers
{    
    /// <summary>
    /// Message handler that allows HTTP Method overrides to suppose clients that don't support PUT,DELETE
    /// </summary>
    public class XHttpMethodOverrideDelegatingHandler : DelegatingHandler
    {
        private const string HttpMethodOverrideHeader = "X-HTTP-Method-Override";

        private static readonly string[] HttpMethods = { "PUT", "DELETE", "POST", "GET" };

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains(HttpMethodOverrideHeader))
            {
                var httpMethod = request.Headers.GetValues(HttpMethodOverrideHeader).FirstOrDefault();
                if (HttpMethods.Contains(httpMethod, StringComparer.InvariantCultureIgnoreCase))
                {
                    request.Method = new HttpMethod(httpMethod);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
