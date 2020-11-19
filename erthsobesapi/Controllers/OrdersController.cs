using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using erthsobesapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Logging;

namespace erthsobesapi.Controllers
{
    [Route("api")]
    public partial class OrderController : Controller
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
            return await CreateObject(dataType);
        }

        [HttpPost]
        [Route("CreateObject/")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<Guid> PostCreateObject([FromBody] string Name, string dataType)
        {
            return await CreateObject(dataType);
        }

        [HttpGet]
        [Route("GetFileById/")]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetObjectById([Bind("id")] Guid id)
        {
            var result = await _dataAccessProvider.GetFileById(id);
            if (result != null)
                if (result.attachment_id != 0)
                    return Ok(result.attachment_id);
                else return NotFound("У запрашиваемого объекта отсутствует файл.");
            else return NotFound("Запрашиваемый объект не найден.");
        }

        [HttpGet]
        [Route("GetStats/")]
        public dynamic GetStats([Bind("id")] Guid id)
        {
            return _dataAccessProvider.GetStats();
        }
    }
}
