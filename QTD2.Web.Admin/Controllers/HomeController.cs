using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QTD2.Web.Admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using QTD2.Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace QTD2.Web.Admin.Controllers
{
   // [Authorize(Policy = Policies.AdminSiteAccess)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var context = HttpContext;
            var user = User;
            return View();
        }
    }
}
