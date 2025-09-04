using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model;

namespace QTD2.API.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Application.Interfaces.Services.Authentication.INotificationService _notificationService;
        private readonly Application.Interfaces.Services.Authentication.IUserService _userService;
        private readonly IUserService _userSharedService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Application.Settings.DomainSettings _domainSettings;
        private readonly IStringLocalizer<UsersController> _localization;
        private readonly Application.Interfaces.Services.Authentication.IInstanceService _instanceService;
        private readonly QTD2.Infrastructure.Metrics.Interfaces.IMetricLogger _eventLogService;
        private readonly Application.Interfaces.Services.Authentication.IIdentityProviderService _identityProviderService;

        public UsersController(
            SignInManager<AppUser> signInManager,
            Application.Interfaces.Services.Authentication.IUserService userService,
            IUserService userSharedService,
            UserManager<AppUser> userManager,
            Application.Interfaces.Services.Authentication.INotificationService notificationService,
            Application.Interfaces.Services.Authentication.IInstanceService instanceService,
            IOptions<Application.Settings.DomainSettings> domainSettings,
            QTD2.Infrastructure.Metrics.Interfaces.IMetricLogger eventLogService,
            IStringLocalizer<UsersController> localization,
            Application.Interfaces.Services.Authentication.IIdentityProviderService identityProviderService)
        {
            _signInManager = signInManager;
            _userService = userService;
            _userSharedService = userSharedService;
            _notificationService = notificationService;
            _domainSettings = domainSettings.Value;
            _userManager = userManager;
            _instanceService = instanceService;
            _localization = localization;
            _eventLogService = eventLogService;
            _identityProviderService = identityProviderService;
        }

        /// <summary>
        /// Gets a list of user available to authenticated user
        /// </summary>
        /// <returns>Http Response Code with user</returns>
        [AllowAnonymous]
        [HttpGet]

        [Route("/users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userSharedService.GetAsync();

            return Ok(new { users });
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/users")]
        public async Task<IActionResult> CreateUserAsync(CreateUserOptions user)
        {
            try
            {
                var userData =  await _userSharedService.CreateUserAsync(user);
                return Ok(new { userData });
            }
            catch(ConflictExceptionHelper ex)
            {
                return Conflict(new { ex.ConflictValue });
            }
        }

        /// <summary>
        /// Gets a user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Http Response Code with user</returns>
        [HttpGet]
        [Route("/users/{name}")]
        public async Task<IActionResult> GetUserAsync(string name)
        {
            var user = await _userSharedService.GetAsync(name);

            return Ok(new { user });
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="name"></param>
        /// <param name="user"></param>
        /// <returns>Http Response Code with message and user</returns>
        [HttpPut]
        [Route("/users/update")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserOptions options)
        {
            try
            {
                var user =  await _userSharedService.UpdateUserAsync(options);
                return Ok(new { user });
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("/users/{name}/instance/{instanceName}")]
        public async Task<IActionResult> RemoveUserByInstanceAsync(string name,string instanceName)
        {
            try
            {
                await _userSharedService.RemoveUserByInstanceAsync(name,instanceName);
                return Ok(new { message = _localization["UserRemoved"] });
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex);
            }
        }

        /// <summary>
        /// Assigns a user to the client
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="username"></param>
        /// <returns>Http Response Code with message</returns>
        [HttpPost]
        [Route("/users/{name}/clients")]
        public async Task<IActionResult> CreateClientUserAsync(string clientName, string username)
        {
            await _userSharedService.AddToClientAsync(clientName, username);
            return Ok(new { message = _localization["UserAssigned"] });
        }

        /// <summary>
        /// Removes a user from the client
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="username"></param>
        /// <returns>Http Response Code with message and user</returns>
        [HttpDelete]
        [Route("/users/{name}/clients")]
        public async Task<IActionResult> DeleteUserClientAsync(string clientName, string username)
        {
            await _userSharedService.RemoveFromClientAsync(clientName, username);
            return Ok(new { message = _localization["User-Unassigned"] });
        }

        private string getMessage(bool is2FaApproved)
        {
            return is2FaApproved ? "LoginSuccess" : "2faRequired"; // matching the message in angular response,will find an alternate later until then we can skip their localization
        }

        [HttpDelete]
        [Route("/users/{name}")]
        public async Task<IActionResult> RemoveUserAsync(string name)
        {
            try
            {
                await _userSharedService.RemoveUserAsync(name);
                return Ok( new { message = _localization["UserRemoved"] });
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("/users/{name}/isAdmin")]
        public async Task<IActionResult> GetUserIsAdminClaimAsync(string name)
        {
            try
            {
                var isAdmin = await _userSharedService.GetUserIsAdminClaimAsync(name);
                return Ok( new { isAdmin });
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
