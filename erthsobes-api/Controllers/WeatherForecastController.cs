using System;
using System.Collections.Generic;
using System.Linq;
using erthsobes_api.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;

namespace erthsobes_api.Controllers
{
    [Route("api")]
    public class OrderController : Controller
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly DataAccessProvider _dataAccessProvider;
        private ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, DataAccessProvider dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("CreateObject/")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<Guid> GetCreateObject([Bind("Name, dataType")] string Name, string dataType)
        {
            var response = await _httpClient.GetAsync("http://172.23.0.4:6500/api/GetObjectInfo?type=" + dataType);
            dynamic newObject = response.Content.ReadAsAsync<ExpandoObject>().Result;
            var newOrder = new Order
            {
                product_id = Guid.Parse(newObject.data.id),
                type = newObject.dataType,
                cost = Convert.ToDecimal(newObject.data.cost)
            };
            switch (newObject.dataType)
            {
                case "phone":
                    newOrder.phoneNumber = newObject.data.phone;
                    break;
                case "email":
                    newOrder.email = newObject.data.email;
                    break;
                default:
                    newOrder.value = newObject.data.value;
                    break;
            }
            try
            {
                newOrder.attachment_id = new Attachment { id = newObject.data.file.id, hash = newObject.data.file.hash };
                await _dataAccessProvider.AddAttachment(newOrder.attachment_id);
                await _dataAccessProvider.AddOrder(newOrder);
                //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", newOrder.id, newOrder.product_id, newOrder.type, newOrder.cost, newOrder.phoneNumber, newOrder.email, newOrder.value);
                //Console.WriteLine(newOrder.attachment_id.id + " " + newOrder.attachment_id.hash);
            }
            catch (RuntimeBinderException)
            {
                await _dataAccessProvider.AddOrder(newOrder);
                //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", newOrder.id, newOrder.product_id, newOrder.type, newOrder.cost, newOrder.phoneNumber, newOrder.email, newOrder.value);
            }
            return newOrder.product_id;
        }

        [HttpPost]
        [Route("CreateObject/")]
        public async Task<IActionResult> PostCreateObject([FromBody] Guid id)
        {
            return Ok(await _dataAccessProvider.GetFileById(id));
        }

        [HttpGet]
        [Route("GetFileById/")]
        public async Task<IActionResult> GetObjectById([Bind("id")] Guid id)
        {
            return Ok(await _dataAccessProvider.GetFileById(id));
        }

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
