using System.Collections.Generic;

namespace Squire.Web.Shared
{
    public interface IWeatherForecastController
    {
        public IEnumerable<WeatherForecast> Get();
    }
}