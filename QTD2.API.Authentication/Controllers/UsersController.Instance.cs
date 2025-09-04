using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QTD2.API.Authentication.Controllers
{
    public partial class UsersController : ControllerBase
    {
        /// <summary>
        /// Gets the client instance which the user of {name} belongs to
        /// </summary>
        /// <param name="name">The name of user</param>
        /// <returns>Http Response Code with user</returns>
        [HttpGet]
        [Route("/users/{name}/instances")]
        public async Task<IActionResult> GetUserInstancesAsync(string name)
        {
            var instances = await _userSharedService.GetUserInstancesAsync(name);
            return Ok(new { instances });
        }
    }
}
