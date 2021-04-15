using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Squire.Web.Shared;

namespace Squire.Web.Server.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        [HttpGet]
        [Route("ApiCalls")]
        public List<MethodInfoAdvise> GetCalls()
        {
            var controller = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(x => x.Name == "WeatherForecastController");
            var method = controller?.GetMethods().Where(x => x.DeclaringType == controller).First();
            var methods = new List<MethodInfoAdvise>
            {
                new MethodInfoAdvise
                {
                    Type = method?.ReturnType.FullName,
                    Name = method?.Name,
                    DeclaringType = method?.DeclaringType?.FullName,
                    Address = "WeatherForecast",
                    HttpMethod = HttpMethod.Get
                }
            };
            return methods;
        }
    }
}
