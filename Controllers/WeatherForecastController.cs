using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SetResponseHeaders;

namespace SetResponseHeaders.Controllers;

[ApiController]
[Route("api/clients")]
public class WeatherForecastController : ControllerBase
{
    private readonly IDataService _dataService;

    public WeatherForecastController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public async IAsyncEnumerable<WeatherForecastData> Get([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var results = await _dataService.GetData();
        // Adding a response header after the calls errors out
        Response.Headers.Add("Test", results.Name);
        await foreach (var result in results.Data.WithCancellation(cancellationToken))
        {
            yield return result;
        }
    }
}
