using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Infrastructure.HttpClients;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Infrastructure.JWT;
using QTD2.Infrastructure.Model.ClientUser;
using QTD2.Infrastructure.Model.Person;
using Microsoft.Extensions.Options;
using DocumentFormat.OpenXml.Spreadsheet;
using QTD2.Infrastructure.Model;
using System;
using QTD2.Domain.Exceptions;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class UserService : Interfaces.Services.Shared.IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJWTBuilder _iJWTBuilder;
        private readonly IClaimsBuilderFactory _claimsBuilderFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IInstanceService _instanceService;
        private readonly IStringLocalizer<UserService> _localizer;
        private readonly QTD2.Domain.Interfaces.Service.Authentication.IIdentityProviderService _identityProviderService;

        public UserService(
            UserManager<AppUser> userManager,
            IJWTBuilder ijwtbuilder,
            IClaimsBuilderFactory identityBuilderFactory,
            IInstanceService instanceService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<UserService> localizer,
            QTD2.Domain.Interfaces.Service.Authentication.IIdentityProviderService identityProviderService)
        {
            _userManager = userManager;
            _iJWTBuilder = ijwtbuilder;
            _claimsBuilderFactory = identityBuilderFactory;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _instanceService = instanceService;
            _localizer = localizer;
            _identityProviderService = identityProviderService;
        }

        public async Task<AppUser> GetAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, user, ClientOperations.Read);
                if (result.Succeeded)
                {
                    return user;
                }
                else
                {
                    throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return user;
        }

        public async Task<string> GetJwtAsync(ClaimsBuilderOptions options, AppUser user)
        {
            //TODO
            //refactor
            var builder = _claimsBuilderFactory.GetBuilder(options);
            var dbClaims = await GetUserClaimsAsync(user);

            var claims = builder.Build(user, dbClaims, options);

            return _iJWTBuilder.CreateJWTTokenString(claims, getJwtOptions(options));
        }

        public async Task<List<Claim>> GetUserClaimsAsync(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            return claims.ToList();
        }

        public async Task<List<AppUser>> GetAsync()
        {
            var users = await Task.Run(() => _userManager.Users);
            users = users.Where(user => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, user, ClientOperations.Read).Result.Succeeded);
            return users.ToList();
        }

        public async Task<AppUser> UpdateUserAsync(UpdateUserOptions options)
        {
            var userExists = await GetAsync(options.OldName);
            if (userExists != null)
            {
                var userWithSameUsername = _userManager.Users.Where(x => x.Email != options.OldName && x.Email == options.NewName);
                if (userWithSameUsername.Any())
                {
                    throw new QTDServerException(displayMessage: _localizer["Employee Username already in use"], false, HttpStatusCode.Conflict);
                }
                if (options.OldName.ToLower() != options.NewName.ToLower())
                {
                    userExists.UserName = options.NewName;
                    userExists.Email = options.NewName;
                    userExists.NormalizedUserName = options.NewName.ToUpper();
                    userExists.NormalizedEmail = options.NewName.ToUpper();

                    var validationResult = await _userManager.UpdateAsync(userExists);
                    if (!validationResult.Succeeded)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                return userExists;
            }
            else
            {
                throw new QTDServerException(displayMessage: _localizer["Record Not Found"], false, HttpStatusCode.NotFound);
            }
        }

        public async Task<AppUser> CreateUserAsync(CreateUserOptions options)
        {
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser()
            { 
                Id = System.Guid.NewGuid().ToString(),
                Email = options.Name,
                UserName = options.Name,
                EmailConfirmed = true,
                TwoFactorEnabled = options.TwoFAEnabled,
                SecurityStamp = System.Guid.NewGuid().ToString(),
                LockoutEnabled = true,
            };
            if (options.Password == "")
            {
                options.Password = System.Guid.NewGuid().ToString();
            }
            user.PasswordHash = hasher.HashPassword(user, options.Password);

            var userExists = await GetAsync(options.Name);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, user, UserOperations.Create);

            if (result.Succeeded)
            {
                if (userExists == null)
                {
                    var validationResult = await _userManager.CreateAsync(user);
                    if (validationResult.Succeeded)
                    {
                        //await _userManager.AddClaimAsync(user, new Claim(Domain.CustomClaimTypes.IsAdmin, "false"));
                        var claims = await _userManager.GetClaimsAsync(user);
                        var hasClaims = claims.Select(s => s.Value).Contains("qtd/claims//instanceName:" + options.InstanceName);
                        if (!hasClaims)
                        {
                            await _userManager.AddClaimAsync(user, new Claim(Domain.CustomClaimTypes.InstanceName, options.InstanceName));
                        }

                        if (!string.IsNullOrEmpty(options.CreateWithIdentityProvider) && options.CreateWithIdentityProvider.Equals("default", StringComparison.OrdinalIgnoreCase))
                        {
                            var instanceSettings = await _instanceService.GetInstanceSettingsAsync(options.InstanceName);

                            if (instanceSettings?.DefaultIdentityProviderId != null)
                            {
                                var identityProvider = await _identityProviderService.GetIdentityProviderByIdAsync(instanceSettings.DefaultIdentityProviderId.Value);

                                if (identityProvider != null)
                                {
                                    var hasIdpClaim = claims.Any(c => c.Type == Domain.CustomClaimTypes.HasIdentityProvider);

                                    if (!hasIdpClaim)
                                    {
                                        await _userManager.AddClaimAsync(user, new Claim(Domain.CustomClaimTypes.HasIdentityProvider, identityProvider.Name));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    var claims = await _userManager.GetClaimsAsync(userExists);
                    var hasClaims = claims.Select(s => s.Value).Contains(options.InstanceName);
                    if (!hasClaims)
                    {
                        await _userManager.AddClaimAsync(userExists, new Claim(Domain.CustomClaimTypes.InstanceName, options.InstanceName));
                    }
                    throw new QTDServerException(displayMessage: "Email Address exists in database", false, HttpStatusCode.Conflict);
                }

            }
            else
            {
                throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
            return user;
        }

        public async Task RemoveUserByInstanceAsync(string name, string instanceName)
        {
            var user = await _userManager.FindByEmailAsync(name);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var adminClaim = claims.Where(r => r.Type == CustomClaimTypes.IsAdmin).FirstOrDefault();
                var isAdmin = adminClaim == null ? false : Convert.ToBoolean(adminClaim.Value);
                var instanceClaims = claims.Where(r => r.Type == CustomClaimTypes.InstanceName);
                var currentInstanceClaim = instanceClaims.Where(x => x.Value == instanceName).FirstOrDefault();
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, user, UserOperations.Update);
                if (result.Succeeded)
                {
                    if (currentInstanceClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, currentInstanceClaim);
                    }
                    if (!isAdmin && !instanceClaims.Where(x => x.Value != instanceName).Any())
                    {
                        //var validationResult = await _userManager.DeleteAsync(user);
                        var guid = Guid.NewGuid().ToString();
                        var deletedName = user.UserName + "." + guid + ".deleted"; // Include a GUID when creating a ".deleted" username so if we delete multiple users by the same name, the UserName is unique per AspNetUser
                        user.UserName = deletedName;
                        user.Email = deletedName;
                        user.NormalizedUserName = deletedName.ToUpper();
                        user.NormalizedEmail = deletedName.ToUpper();

                        var validationResult = await _userManager.UpdateAsync(user);
                        if (!validationResult.Succeeded)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                }
                else
                {
                    throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["User Not Found"], StatusCodes.Status404NotFound);
            }
        }
        public async Task RemoveUserAsync(string name)
        {
            var user = await _userManager.FindByEmailAsync(name);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var adminClaim = claims.Where(r => r.Type == CustomClaimTypes.IsAdmin).FirstOrDefault();
                var isAdmin = adminClaim == null ? false : Convert.ToBoolean(adminClaim.Value);
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, user, UserOperations.Delete);
                if (result.Succeeded)
                {
                    if (!isAdmin)
                    {
                        //var validationResult = await _userManager.DeleteAsync(user);
                        var guid = Guid.NewGuid().ToString();
                        var deletedName = user.UserName + "." + guid + ".deleted"; // Include a GUID when creating a ".deleted" username so if we delete multiple users by the same name, the UserName is unique per AspNetUser
                        user.UserName = deletedName;
                        user.Email = deletedName;
                        user.NormalizedUserName = deletedName.ToUpper();
                        user.NormalizedEmail = deletedName.ToUpper();

                        var validationResult = await _userManager.UpdateAsync(user);
                        if (!validationResult.Succeeded)
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }
                    }
                }
                else
                {
                    throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["User Not Found"], StatusCodes.Status404NotFound);
            }
        }

        public async Task<List<Instance>> GetUserInstancesAsync(string name)
        {
            var user = await _userManager.FindByEmailAsync(name);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var instances = (await _instanceService.GetAsync()).ToList();
                var instanceClaims = claims.Where(r => r.Type == CustomClaimTypes.InstanceName).ToList();
                List<string> instanceNames = instanceClaims.Select(r => r.Value).ToList();
                List<Instance> availableIstances = instances.Where(x => instanceNames.Contains(x.Name)).ToList();
                return availableIstances;
            }
            else
            {
                return new List<Instance>();
            }
        }

        public async Task AddToClientAsync(string clientName, string username)
        {
            // var client = await _clientService.GetAsync(clientName);

            // var person = (await _personService.GetAsync()).FirstOrDefault(x => x.Username == username);

            // var clientUser = _clientUserService.CreateAsync(new ClientUserCreateOptions
            // {
            //    PersonId = person.Id,
            // });
            await Task.CompletedTask;
        }

        public async Task RemoveFromClientAsync(string clientName, string username)
        {
            // var client = await _clientService.GetAsync(clientName);

            // var person = (await _personService.GetAsync()).FirstOrDefault(x => x.Username == username);

            // await _clientUserService.DeleteAsync(person.Id);
            await Task.CompletedTask;
        }

        private JWTOptions getJwtOptions(ClaimsBuilderOptions options)
        {
            // TODO refactor these settings into app.config
            if (options.IsRefreshToken)
            {
                return new JWTOptions()
                {
                    ExpirationMinutes = 180,
                };
            }
            else
            {
                return new JWTOptions()
                {
                    ExpirationMinutes = 15,
                };
            }
        }

        public async Task<bool> GetUserIsAdminClaimAsync(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var adminClaim = claims.Where(r => r.Type == CustomClaimTypes.IsAdmin).FirstOrDefault();
                return adminClaim == null ? false : Convert.ToBoolean(adminClaim.Value);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["User Not Found"], StatusCodes.Status404NotFound);
            }
        }
    }
}
