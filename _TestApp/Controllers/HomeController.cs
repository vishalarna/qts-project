using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _TestApp.Models;

namespace _TestApp.Controllers
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
            bool s = shouldHash("application/json", new Uri("https://sqltest.qtsserver.com/QTD2/App/implementation/employees"));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool shouldHash(string contentType, Uri referrer)
        {
            string s = referrer.ToString();
            var spaUri = new Uri("https://sqltest.qtsserver.com/QTD2/App/");

            string path = spaUri.PathAndQuery;

            var directory = new Uri(referrer, System.IO.Path.GetDirectoryName(referrer.AbsolutePath));
            string t = directory.OriginalString;

            //UriComponents.
            int compare = Uri.Compare(referrer, spaUri, UriComponents.Host | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);
            _logger.LogInformation($"Asking should hash {contentType} and {referrer.ToString()}");

            return (contentType ?? "").ToUpper().Contains("APPLICATION/JSON") && Uri.Compare(referrer, spaUri, UriComponents.Host | UriComponents.Path, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
