using Leaderboard.Web.DelegatingHandlers;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Leaderboard.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // http://www.asp.net/web-api/overview/working-with-http/http-message-handlers
            // http://www.hanselman.com/blog/HTTPPUTOrDELETENotAllowedUseXHTTPMethodOverrideForYourRESTServiceWithASPNETWebAPI.aspx
            GlobalConfiguration.Configuration.MessageHandlers.Add(new XHttpMethodOverrideDelegatingHandler());

            // http://stackoverflow.com/questions/11979637/what-does-referenceloophandling-ignore-in-newtonsoft-json-exactly-do
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            // http://www.asp.net/web-api/overview/web-api-routing-and-actions
            config.MapHttpAttributeRoutes();

            // Enable CORS
            // http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            config.EnableCors();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
