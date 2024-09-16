using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetResponseHeaders;

public class DataService : IDataService
{
    public async Task<WeatherForecast> GetData()
    {
        var testData = new List<WeatherForecastData>
        {
            new()
            {
                Name = "Freezing",
                Total = 100
            },
            new()
            {
                Name = "Freezing",
                Total = 100
            },
            new()
            {
                Name = "Freezing",
                Total = 100
            }
        };
        return new WeatherForecast
        {
            Name = "Weather Name Test",
            Data = testData.ToAsyncEnumerable()
        };
    }
}