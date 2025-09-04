using Microsoft.AspNetCore.Mvc;
using QTD2.Application.HttpClients;

using QTD2.Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace QTD2.Web.Admin.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Policy = Policies.AdminSiteAccess)]
    public class UsersController : Controller
    {
        private readonly QtdAuthenticationService _qtdAuthenticationService;

        public UsersController(QtdAuthenticationService qtdAuthenticationService)
        {
            _qtdAuthenticationService = qtdAuthenticationService;
        }

        public JsonResult Get()
        {
            //use the Shared.IUsersService, not the HTTP client directly

            throw new System.NotImplementedException();
            var users = _qtdAuthenticationService.Users.Get();
           // clients = clients.Where(client => _authorizationService.AuthorizeAsync(User, client, ClientOperations.Read).Result.Succeeded).ToList();
           // return new JsonResult(clients);
        }
    }
}
