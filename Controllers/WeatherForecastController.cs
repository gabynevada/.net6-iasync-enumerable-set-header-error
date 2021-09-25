using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SetResponseHeaders;
using SetResponseHeaders.Db;

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

    [HttpGet("possible-solution")]
    public async Task<IAsyncEnumerable<WeatherForecastData>> Solution()
    {
        var results = await _dataService.GetData();

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
    
    [HttpPost("data")]
    public async Task<IActionResult> AddData()
    {
        await _dataService.AddData();
        return Ok();
    }
    
    [HttpGet("pipeline-error")]
    public IAsyncEnumerable<Data> PipelineError()
    {
        return _dataService.GetDataFromDb();
    }
    
    [HttpGet("possible-solution-with-pipeline-error")]
    public IAsyncEnumerable<Data> SolutionWithPipelineError()
    {
        var results = _dataService.GetDataFromDb();;
        
        return Enumerate();

        async IAsyncEnumerable<Data> Enumerate()
        {
            await foreach (var result in results)
            {
                yield return result;
            }
        }
    }
}
