using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using erthsobesservice.Model;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Swashbuckle.Swagger.Annotations;
using System.Text.Json.Serialization;

namespace erthsobesservice.Controllers
{
    [ApiController]
    [Route("api")]
    public partial class ServiceController : ControllerBase
    {

        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Возвращает информацию об объекте, по его типу
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET http://{host}:{port}/api/GetObjectInfo?type=phone
        ///
        /// </remarks>
        /// <param name="type">
        /// Тип запрашиваемого объекта: phone, email или 0, 1.
        /// Все, что не относится к перечисленному, будет отнесено к типу "other".
        /// </param>
        /// <returns></returns>
        /// <response code="200">
        ///    {
        ///         "dataType":"phone",
        ///         "data":
        ///         "{
        ///             \"PhoneNumber\":\"89392975637\",
        ///             \"Id\":\"34b57891-71ce-43e7-b49f-b243d7f851e6\",
        ///             \"Cost\":79.58
        ///          }"
        ///     }
        /// </response>
        /// <response code="500">
        ///    {
        ///         "dataType":"",
        ///         "data":
        ///         "{
        ///             \"Value\":\"\",
        ///             \"Id\":\"\",
        ///             \"Cost\":\"\"
        ///          }"
        ///          "error":"Описание ошибки"
        ///     }
        /// </response>
        [HttpGet]
        [Route("GetObjectInfo/")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerResponse(HttpStatusCode.OK, "Возвращает информацию об объекте, по его типу")]
        public IActionResult GetObjectInfo([Bind("type")] string type)
        {
            try
            {
                var file = GetFile();
                switch (type)
                {
                    case "phone":
                    case "0":

                        return (file is null) ?
                            Ok(new { dataType = "phone", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Phone = "89220333338" } })
                        :
                            Ok(new { dataType = "phone", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Phone = "89220333338", File = file } });

                    case "email":
                    case "1":

                        return (file is null) ?
                            Ok(new { dataType = "email", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Email = "test@test.com" } })
                            :
                            Ok(new { dataType = "email", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Email = "test@test.com", File = file } });

                    default:
                        return (file is null) ?
                            Ok(new { dataType = "other", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Value = "Описание объекта" } })
                            :
                            Ok(new { dataType = "other", data = new { Id = Guid.NewGuid(), Cost = GetCost(), Value = "Описание объекта", File = file } });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { dataType = "", data = new { Id = "", Cost = "", Value = "" }, error = e });
            }
        }

        /// <summary>
        /// Возвращает файл по Id файла и hash.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST http://{host}:{port}/Api/GetFile
        ///     {
        ///         "id":1,
        ///         "hash":"c8293c1122925a4a56ef572d280ffe587f5f003917b621b3166ab0bce438a793"
        ///     }
        /// </remarks>
        /// <param>
        ///     {
        ///         "id":1,
        ///         "hash":"c8293c1122925a4a56ef572d280ffe587f5f003917b621b3166ab0bce438a793"
        ///     }
        /// </param>
        /// <returns>Возвращает поток с данными file</returns>
        /// 
        [HttpPost]
        [Route("GetFile/")]
        [SwaggerResponse(HttpStatusCode.OK, "Возвращает поток с данными file", typeof(Attachment))]
        [SwaggerResponse(HttpStatusCode.Forbidden)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public ActionResult GetFile([FromBody] Attachment file)
        {
            try
            {
                if (file.id == 0 || file.hash == null)
                    return StatusCode(404);
                if (!IsMD5(file.hash))
                    return StatusCode(403);
                MemoryStream stream = SerializeToStream(file);
                byte[] vs = stream.ToArray();
                return File(vs, "application/json", file.id.ToString() + ".json");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
