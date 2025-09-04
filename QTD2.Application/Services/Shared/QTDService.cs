using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.QtdUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAdminMessageDomainService = QTD2.Domain.Interfaces.Service.Core.IAdminMessageService;
using IAdminMessageQTDUserDomainService = QTD2.Domain.Interfaces.Service.Core.IAdminMessage_QTDUserService;

namespace QTD2.Application.Services.Shared
{
    public class QTDService : IQTDService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IQTDUserService _qTDUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<QTDService> _localizer;
        private readonly IAdminMessageDomainService _adminMessageDomainService;
        private readonly IAdminMessageQTDUserDomainService _adminMessageQTDUserDomainService;
        public QTDService(IQTDUserService qTDUserService, IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IStringLocalizer<QTDService> localizer, IAdminMessageDomainService adminMessageDomainService, IAdminMessageQTDUserDomainService adminMessageQTDUserDomainService)
        {
            _qTDUserService = qTDUserService;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _localizer = localizer;
            _adminMessageDomainService = adminMessageDomainService;
            _adminMessageQTDUserDomainService = adminMessageQTDUserDomainService;
        }
        public async Task<List<QtdUserVM>> GetAllActiveAsync()
        {
            var allQtdUsers = await _qTDUserService.GetAllActive();
            var qtdUsers = allQtdUsers.Select(x => new QtdUserVM(x.Id, x.Person));
            return qtdUsers.OrderBy(x => x.Person?.FirstName).ToList();
        }


        public async Task<QtdUserVM> CreateAsync(QtdUserVM qtdUserOption, bool isReturnConflictExp = false)
        {
            if (qtdUserOption == null || qtdUserOption.Person == null)
                throw new ArgumentNullException(nameof(qtdUserOption));
            else
            {
                var qtdUserData = (await _qTDUserService.FindAsync(x => x.PersonId == qtdUserOption.Person.Id)).FirstOrDefault();
                if (qtdUserData == null)
                {
                    var qtdUser = new QTDUser(qtdUserOption.Person.Id);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, qtdUser, QTDUserOperations.Create);
                    if (result.Succeeded)
                    {
                        qtdUser.Create((await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id);
                        var validationResult = await _qTDUserService.AddAsync(qtdUser);
                        if (validationResult.IsValid)
                        {                            
                            qtdUser = (await _qTDUserService.GetWithIncludeAsync(qtdUser.Id, new string[] { "Person" }));
                            await AddAdminMessageForNewQTDUser(qtdUser.Id);
                            return new QtdUserVM(qtdUser.Id, qtdUser.Person);
                        }
                        else
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }

                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
                else
                {
                    var qtdUserExist = new QtdUserVM(qtdUserData.Id, qtdUserData.Person);
                    if (isReturnConflictExp)
                    {
                        throw new ConflictExceptionHelper(qtdUserExist);
                    }
                    else
                    {
                        return qtdUserExist;
                    }
                }

            }
        }

        public async Task<QtdUserVM> UpdateAsync(int id, QtdUserVM qtdUserOption)
        {
            if (qtdUserOption == null || qtdUserOption.Person == null)
                throw new ArgumentNullException(nameof(qtdUserOption));
            else
            {
                var person = qtdUserOption.Person;
                QTDUser qtdUser = await _qTDUserService.GetAsync(id);
                if (qtdUser != null)
                {
                    qtdUser.setPersonId(person.Id);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, qtdUser, QTDUserOperations.Update);
                    if (result.Succeeded)
                    {
                        var validationResult = await _qTDUserService.UpdateAsync(qtdUser);
                        if (validationResult.IsValid)
                        {
                            qtdUser = (await _qTDUserService.GetWithIncludeAsync(qtdUser.Id, new string[] { "Person" }));
                            return new QtdUserVM(qtdUser.Id, qtdUser.Person);
                        }
                        else
                        {
                            throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                        }

                    }
                    else
                    {
                        throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                    }
                }
                else
                {
                    throw new BadHttpRequestException(message: _localizer["QTDUserNotFound"]);
                }
            }
        }

        public async Task<QtdUserVM> ActivateAsync(int id)
        {
            QTDUser qtdUser = await _qTDUserService.GetAsync(id);
            if (qtdUser != null)
            {
                qtdUser.Activate();
                await _qTDUserService.UpdateAsync(qtdUser);
                qtdUser = (await _qTDUserService.GetWithIncludeAsync(qtdUser.Id, new string[] { "Person" }));
                return new QtdUserVM(qtdUser.Id, qtdUser.Person);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["QTDUserNotFound"]);
            }

        }

        public async Task<QtdUserVM> DeactivateAsync(int id)
        {
            QTDUser qtdUser = await _qTDUserService.GetAsync(id);
            QtdUserVM qtdUserVm = new QtdUserVM();
            if (qtdUser != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, qtdUser, QTDUserOperations.Update);
                if (result.Succeeded)
                {
                    qtdUser.Deactivate();
                    var validationResult = await _qTDUserService.UpdateAsync(qtdUser);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    qtdUser = (await _qTDUserService.GetWithIncludeAsync(qtdUser.Id, new string[] { "Person" }));
                    qtdUserVm =  new QtdUserVM(qtdUser.Id, qtdUser.Person);
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return qtdUserVm;
        }

        public async Task<QTDUser> GetQTDUserByUsernameAsync(string username)
        {
            return await _qTDUserService.GetQTDUserByUsername(username);
        }

        public async System.Threading.Tasks.Task AddAdminMessageForNewQTDUser(int id)
        {
            var messages = (await _adminMessageDomainService.GetAllAdminMessageAsync()).GroupBy(x => x.Message).Select(g => g.First()).ToList();
            var adminMessages = new List<AdminMessage_QTDUser>();
            foreach (var message in messages)
            {
                var adminMessage = new AdminMessage_QTDUser
                {
                    AdminMessageId = message.Id,
                    QTDUserId = id,
                    Dismissed = false

                };
                adminMessages.Add(adminMessage);
            }
            await _adminMessageQTDUserDomainService.AddRangeAsync(adminMessages);
        }
    }
}
