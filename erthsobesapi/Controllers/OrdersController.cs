using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using erthsobesapi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

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


        /// <summary>
        /// По dataType получает новый объект из API сторонней системы (erthsobesservice) и сохраняет его в БД.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///         GET http://{host}:{port}/Api/CreateObject/?Name=John&amp;dataType=phone
        ///         
        /// Response:
        /// 
        ///        "b1510dd2-a738-45ce-ad20-41933ef97704"     
        /// 
        /// </remarks>
        /// <param name="Name">Имя запрашивающей стороны</param>
        /// <param name="dataType">Тип объекта ( phone, email, other, 0, 1)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CreateObject/")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<Guid> GetCreateObject([Bind("Name, dataType")] string Name, string dataType)
        {
            return await CreateObject(dataType);
        }

        /// <summary>
        /// По dataType получает новый объект из API сторонней системы (erthsobesservice) и сохраняет его в БД.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///         POST http://{host}:{port}/Api/CreateObject
        ///         {
        ///            "Name":"John",
        ///            "dataType":"1"
        ///         }
        ///         
        /// Response:
        ///         
        ///         "b1510dd2-a738-45ce-ad20-41933ef97704"
        /// 
        /// </remarks>
        /// <param>
        ///  {
        ///      "Name":"Имя запрашивающей стороны",
        ///      "dataType":"Тип объекта ( phone, email, other, 0, 1)"
        ///  }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateObject/")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        public async Task<Guid> PostCreateObject([FromBody] string Name, string dataType)
        {
            return await CreateObject(dataType);
        }

        /// <summary>
        /// По Id объекта, полученному в первом сервисе, выгружает файл
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///         GET http://{host}:{port}/Api/GetFileById/?id=b1510dd2-a738-45ce-ad20-41933ef97704
        ///         
        /// </remarks>
        /// <param name="id">Id объекта, для которого запрашивается файл</param>
        /// <returns>Возвращает поток с данными file</returns>
        [HttpGet]
        [Route("GetFileById/")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetObjectById([Bind("id")] Guid id)
        {
            var result = await _dataAccessProvider.GetFileById(id);
            if (result != null)
                if (result.attachment_id != 0)
                {
                    Attachment attachment = await _dataAccessProvider.GetAttachment(result.attachment_id);
                    MemoryStream stream = SerializeToStream(attachment);
                    byte[] vs = stream.ToArray();
                    return File(vs, "application/json", result.attachment_id.ToString() + ".json");
                }
                else return NotFound("У запрашиваемого объекта отсутствует файл.");
            else return NotFound("Запрашиваемый объект не найден.");
        }

        /// <summary>
        ///  Возвращает общую статистику по сохраненным в базу объектам в виде json.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET http://{host}:{port}/Api/GetStats
        /// 
        /// Response: 
        /// 
        ///        {
        ///           "PhoneCount":"1",
        ///           "PhoneCountWhitFile":"1",
        ///           "TopPhones":"88008008000;88008008001",
        ///           "EmailСount":"1",
        ///           "EmailCountWhitFile":"1",
        ///           "TopEmails":"test@test.com;test1@.test.com",
        ///           "OhterCount":"10",
        ///           "OhterCountWhitFile":"15",
        ///        }
        ///
        /// </remarks>
        /// <returns>
        /// 
        /// 
        /// </returns>
        [HttpGet]
        [Route("GetStats/")]
        public dynamic GetStats()
        {
            return _dataAccessProvider.GetStats();
        }
    }
}
