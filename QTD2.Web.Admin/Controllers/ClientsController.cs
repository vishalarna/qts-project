using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QTD2.Infrastructure.Authorization.Operations;
using QTD2.Application.Interfaces.Services.Shared;
using Microsoft.EntityFrameworkCore;

using QTD2.Application.Authorization;

using System.Linq;

namespace QTD2.Web.Admin.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = Policies.AdminSiteAccess)]
    public class ClientsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IClientService _clientService;
        private readonly IDbContextBuilder _dbContextBuilder;

        public ClientsController(IAuthorizationService authorizationService,
                                IClientService clientService,
                                IDbContextBuilder dbContextBuilder)
        {
            _clientService = clientService;
            _authorizationService = authorizationService;
            _dbContextBuilder = dbContextBuilder;
        }

        public IActionResult Index()
        {           
            return View();
        }

        public IActionResult Client(string name)
        {
            var client = _clientService.Get(name);
            return View(client);
        }

        public JsonResult Get()
        {
            var clients = _clientService.Get();
            clients = clients.Where(client => _authorizationService.AuthorizeAsync(User, client, ClientOperations.Read).Result.Succeeded).ToList();
            return new JsonResult(clients);
        }

        [HttpPost]
        public IActionResult Create(Domain.Entities.Authentication.Client client)
        {
            var result = _authorizationService.AuthorizeAsync(User, client, ClientOperations.Create).Result;

            if (result.Succeeded)
            {
                var validationResult = _clientService.CreateClient(client);

                if (validationResult.IsValid)
                    return new JsonResult(client);

                else return new JsonResult(validationResult.Errors);
            }
            else
            {
                return StatusCode(403);
            }
        }

        [HttpPost]
        public IActionResult CreateDatabase(string Name)
        {
            var client = _clientService.Get(Name);

            var result = _authorizationService.AuthorizeAsync(User, client, ClientOperations.CreateDatabase).Result;

            if (result.Succeeded)
            {
                var context = _dbContextBuilder.BuildQtdContext(client.Name, true);
                context.Database.Migrate();

                return new JsonResult(client);
            }
            else
            {
                return StatusCode(403);
            }
        }
    }
}
