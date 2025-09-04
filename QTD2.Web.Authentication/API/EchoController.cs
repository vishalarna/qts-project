using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Web.Authentication.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        public string Get(string message)
        {
            return message;
        }

    }
}
