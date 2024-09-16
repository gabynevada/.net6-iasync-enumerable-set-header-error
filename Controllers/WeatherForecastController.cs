using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SetResponseHeaders.Controllers;

[ApiController]
[Route("api/data")]
public class WeatherForecastController(IDataService dataService) : ControllerBase
{
    [HttpGet("set-header-aysnc-enumerable-error")]
    public async IAsyncEnumerable<WeatherForecastData> GetLocal([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var results = await dataService.GetData();
        // Adding a response header before yielding first result errors out
        Response.Headers.Add("Test", results.Name);
        await foreach (var result in results.Data.WithCancellation(cancellationToken))
        {
            yield return result;
        }
    }
    
    [HttpGet("possible-solution")]
    public async Task<IAsyncEnumerable<WeatherForecastData>> Solution()
    {
        var results = await dataService.GetData();

        // Adding a response header after the calls works now
        Response.Headers.Add("Test", results.Name);

        return Enumerate();

        async IAsyncEnumerable<WeatherForecastData> Enumerate()
        {
            await foreach (var result in results.Data)
            {
                yield return result;
            }
        }
    }
}
