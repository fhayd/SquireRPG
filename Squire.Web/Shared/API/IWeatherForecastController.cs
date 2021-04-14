using System.Collections.Generic;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Annotation;

namespace Squire.Web.Shared.API
{
    [DynamicHandle]
    public interface IWeatherForecastController
    {
        public Task<IEnumerable<WeatherForecast>> Get();
    }
}