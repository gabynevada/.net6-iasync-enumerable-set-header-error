using System.Threading.Tasks;

namespace SetResponseHeaders;

public interface IDataService
{
    Task<WeatherForecast> GetData();
}
