using System;
using System.Collections.Generic;
using System.Linq;
using erthsobes_api.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace erthsobes_api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        //private readonly IDataAccessProvider _dataAccessProvider;

        //public OrderController(IDataAccessProvider dataAccessProvider)
        //{
        //    _dataAccessProvider = dataAccessProvider;
        //}



        //[HttpGet]
        //[Route("Attachments")]
        //public IEnumerable<Attachment> GetAttachments()
        //{
        //    return _dataAccessProvider.GetAttachments();
        //}

        //[HttpPost]
        //public void Post([FromBody]Attachment value)
        //{
        //    _dataAccessProvider.AddAttachment(value);
        //}
    }

    //[ApiController]
    //[Route("[controller]")]
    //public class WeatherForecastController : ControllerBase
    //{
    //    private static readonly string[] Summaries = new[]
    //    {
    //        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //    };

    //    private readonly ILogger<WeatherForecastController> _logger;

    //    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    [HttpGet]
    //    public IEnumerable<WeatherForecast> Get()
    //    {
    //        var rng = new Random();
    //        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //        {
    //            Date = DateTime.Now.AddDays(index),
    //            TemperatureC = rng.Next(-20, 55),
    //            Summary = Summaries[rng.Next(Summaries.Length)]
    //        })
    //        .ToArray();
    //    }
    //}


}
