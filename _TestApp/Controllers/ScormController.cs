using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _TestApp.Controllers
{
    public class ScormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
