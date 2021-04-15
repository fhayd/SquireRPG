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
            var response = await client.GetAsync("ApiCalls");
            if (response.IsSuccessStatusCode)
            {
                var methodInfos = await response.Content.ReadAsStringAsync();
                ApiHelper.SetAddresses(
                    JsonConvert.DeserializeObject<IEnumerable<MethodInfoAdvise>>(methodInfos));
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
                var method = (MethodInfo)context.TargetMethod;
                var call = ApiHelper.GetCall(method);
                var response = await _client.SendAsync(new HttpRequestMessage(call.HttpMethod, call.Address));
                // if (method.ReturnType.IsAssignableFrom(typeof(Task<>)))
                // {
                    context.ReturnValue = Task.FromResult(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(),
                        method.ReturnType.GetGenericArguments()[0]));
                // }
            }
        }
    }
}