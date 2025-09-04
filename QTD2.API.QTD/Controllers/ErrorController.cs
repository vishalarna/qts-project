using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using QTD2.Infrastructure.Model.Error;
using Serilog;
using Serilog.Formatting.Json;

namespace QTD2.API.QTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class ErrorController : Controller
    {
        private ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/Error")]
        public IActionResult LogErrorAsync([FromBody] ErrorOptions error)
        {
            // _logger.LogError("I am here");
            // var log = new LoggerConfiguration()
            //    .WriteTo.File(new JsonFormatter(), "./client_logs/client_errors.json").CreateLogger();
            Log.ForContext<ErrorController>().Error(string.Format("Error : {0}", error.errorInfo));
            return Ok( new { message = "Error stored" });
        }
    }
}
