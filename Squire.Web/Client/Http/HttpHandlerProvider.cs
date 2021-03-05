using System;
using System.Reflection;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Squire.Web.Shared;

namespace Squire.Web.Client.Http
{
    public class HttpHandlerProvider
    {
        public async Task<IWeatherForecastController> CreateForecastHttpHandler()
        {
            var aspect = new HttpInvoke();
            return aspect.Handle<IWeatherForecastController>();
        }
        private class HttpInvoke : Attribute, IMethodAdvice
        {
            public void Advise(MethodAdviceContext context)
            {
                context.Proceed();
            }

            // private object Invoke(MethodAdviceContext targetMethod)
            // {
            //     return targetMethod.
            // }
        }
    }

    
}