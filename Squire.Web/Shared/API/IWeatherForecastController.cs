using System.Collections.Generic;
using ArxOne.MrAdvice.Annotation;

namespace Squire.Web.Shared.API
{
    [DynamicHandle]
    public interface IWeatherForecastController
    {
        public IEnumerable<WeatherForecast> Get();
    }
}