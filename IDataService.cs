
using System.Collections.Generic;
using System.Threading.Tasks;
using SetResponseHeaders.Db;

namespace SetResponseHeaders
{
    public interface IDataService
    {
        Task<WeatherForecast> GetData();
        IAsyncEnumerable<Data> GetDataFromDb();
        Task AddData();
    }
}