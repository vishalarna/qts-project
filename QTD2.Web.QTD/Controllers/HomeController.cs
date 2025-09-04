using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QTD2.Web.QTD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Web.QTD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = User;
            return View();
        }
    }
}
