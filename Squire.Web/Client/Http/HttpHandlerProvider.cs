using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Newtonsoft.Json;
using Squire.Web.Shared;
using Squire.Web.Shared.API;

namespace Squire.Web.Client.Http
{
    public class HttpHandlerProvider
    {
        public async Task<IWeatherForecastController> CreateForecastHttpHandler(HttpClient client)
        {
            var aspect = new HttpInvoke(client);
            var addresses = await client.GetAsync("ApiCalls");
            if (addresses.IsSuccessStatusCode)
            {
                var content = await addresses.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                // ApiHelper.SetAddresses(
                //     JsonConvert.DeserializeObject<IEnumerable<(MethodInfo, MethodInfoAdvise)>>(content));
            }

            return aspect.Handle<IWeatherForecastController>();
        }

        private class HttpInvoke : Attribute, IMethodAsyncAdvice
        {
            private HttpClient _client;

            public HttpInvoke(HttpClient client)
            {
                _client = client;
            }

            public async Task Advise(MethodAsyncAdviceContext context)
            {
                var weather = await _client.GetAsync("WeatherForecast");
                var method = (MethodInfo) context.TargetMethod;
                // if (method.ReturnType.IsAssignableFrom(typeof(Task<>)))
                // {
                var genericReturnValue = JsonConvert.DeserializeObject(await weather.Content.ReadAsStringAsync(),
                    method.ReturnType.GetGenericArguments()[0]);
                context.ReturnValue = Task.FromResult(genericReturnValue);
                        ;
                // }
            }
        }
    }
}