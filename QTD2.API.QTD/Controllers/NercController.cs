using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QTD2.Application.Interfaces.Services.Shared;

namespace QTD2.API.QTD.Controllers
{
    [ApiController]
    public partial class NercController : Controller
    {
        INercService _nercService;

        public NercController(INercService nercService)
        {
            _nercService = nercService;
        }
    }
}
