using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using erthsobesservice.Model;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

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

        [HttpGet]
        [Route("GetObjectInfo/")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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

        [HttpPost]
        [Route("GetFile/")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public HttpResponseMessage GetFile([FromBody] Attachment file)
        {
            try
            {
                if (file.id == 0 || file.hash == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                if (!IsMD5(file.hash))
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                MemoryStream stream = SerializeToStream(file);
                Console.WriteLine(stream.ToArray());
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };
                result.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "file"
                    };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return result;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
