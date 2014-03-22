using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Leaderboard.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            // http://www.asp.net/web-api/overview/web-api-routing-and-actions
            config.MapHttpAttributeRoutes();

            // Enable CORS
            // http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
