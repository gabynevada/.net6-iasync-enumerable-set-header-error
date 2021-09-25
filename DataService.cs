using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SetResponseHeaders.Db;

namespace SetResponseHeaders
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            this._context = context;
        }

        public IAsyncEnumerable<Data> GetDataFromDb()
        {
            return _context.Data.AsAsyncEnumerable();
        }

        public async Task AddData()
        {
            var data = new List<Data>
            {
                new()
                {
                    DataId = Guid.NewGuid(),
                    Name = "Test",
                    Number = 5,
                    StartDate = DateTime.UtcNow
                },
                new()
                {
                    DataId = Guid.NewGuid(),
                    Name = "Test",
                    Number = 5,
                    StartDate = DateTime.UtcNow
                },
                new()
                {
                    DataId = Guid.NewGuid(),
                    Name = "Test",
                    Number = 5,
                    StartDate = DateTime.UtcNow
                },
                new()
                {
                    DataId = Guid.NewGuid(),
                    Name = "Test",
                    Number = 5,
                    StartDate = DateTime.UtcNow
                },
                new()
                {
                    DataId = Guid.NewGuid(),
                    Name = "Test",
                    Number = 5,
                    StartDate = DateTime.UtcNow
                }
            };
            await _context.Data.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
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
}