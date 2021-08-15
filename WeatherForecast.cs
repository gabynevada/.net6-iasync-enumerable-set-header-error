using System.Collections.Generic;
using SetResponseHeaders;

namespace SetResponseHeaders;

public class WeatherForecast
{
    public string? Name { get; set; }

    public IAsyncEnumerable<WeatherForecastData> Data { get; set; }
}
