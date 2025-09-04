using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.SafetyHazard_Set;
using ISafetyHazard_SetDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_SetService;
using ISafetyHazardDomainService = QTD2.Domain.Interfaces.Service.Core.ISaftyHazardService;
using ISafetyHazard_Set_LinkDomainService = QTD2.Domain.Interfaces.Service.Core.ISafetyHazard_Set_LinkService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class SafetyHazard_SetService : Interfaces.Services.Shared.ISafetyHazard_SetService
    {
        private readonly IStringLocalizer<SafetyHazard_SetService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISafetyHazard_SetDomainService _safetyHazardSetService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ISafetyHazardDomainService _shService;
        private readonly ISafetyHazard_Set_LinkDomainService _sh_set_LinkService;
        private readonly SafetyHazard_Set_Link _sh_set_link;

        public SafetyHazard_SetService(IStringLocalizer<SafetyHazard_SetService> localizer, UserManager<AppUser> userManager, ISafetyHazard_SetDomainService safetyHazardSetService, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, ISafetyHazardDomainService shService, ISafetyHazard_Set_LinkDomainService sh_set_LinkService)
        {
            _localizer = localizer;
            _userManager = userManager;
            _safetyHazardSetService = safetyHazardSetService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _shService = shService;
            _sh_set_LinkService = sh_set_LinkService;
            _sh_set_link = new SafetyHazard_Set_Link();
        }

        public async System.Threading.Tasks.Task DeleteSafetyHazardSet(int id)
        {
            var shSet = await _safetyHazardSetService.GetAsync(id);
            if (shSet == null)
            {
                throw new QTDServerException(_localizer["SafetyHazardSetNotFound"]);
            }
            else
            {
                shSet.Delete();
                var validationResult = await _safetyHazardSetService.UpdateAsync(shSet);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
        }

        public async Task<List<SafetyHazard_Set>> GetSafetyHazardSets()
        {
            var shSet = await _safetyHazardSetService.AllAsync();
            return shSet.ToList();
        }

        public async Task<SafetyHazard_Set> GetSafetyHazardSets(int id)
        {
            var shSet = await _safetyHazardSetService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            return shSet;
        }

        public async Task<List<SafetyHazard_Set>> GetSetForSH(int id)
        {
            var shSets = await _sh_set_LinkService.AllQueryWithInclude(new string[] { nameof(_sh_set_link.SafetyHazardSet) }).Where(x => x.SafetyHazardId == id).Select(x => x.SafetyHazardSet).OrderBy(x => x.Id).ToListAsync();
            return shSets;
        }

        public async Task<SafetyHazard_Set> CreateSafetyHazardSet(SafetyHazard_SetCreateOptions options)
        {
            var obj = (await _safetyHazardSetService.FindAsync(x => x.SafetyHzAbatementText == options.SafetyHzAbatementText && x.SafetyHzControlsText == options.SafetyHzControlsText)).FirstOrDefault();
            if (obj != null)
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            obj = new SafetyHazard_Set(options.SafetyHzAbatementText, options.SafetyHzAbatementFiles, options.SafetyHzAbatementImage, options.SafetyHzControlsText, options.SafetyHzControlsFiles, options.SafetyHzControlsImage);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, SafetyHazard_SetOperations.Create);
            if (result.Succeeded)
            {
                obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.CreatedDate = DateTime.Now;
                var validationResult = await _safetyHazardSetService.AddAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }

        public async Task<SafetyHazard_Set> CreateAndLinkWithSH(int id, SafetyHazard_SetCreateOptions options)
        {
            var shSet = new SafetyHazard_Set();
            shSet.SafetyHzAbatementFiles = options.SafetyHzAbatementFiles;
            shSet.SafetyHzAbatementImage = options.SafetyHzAbatementImage;
            shSet.SafetyHzAbatementText = options.SafetyHzAbatementText;
            shSet.SafetyHzControlsFiles = options.SafetyHzControlsFiles;
            shSet.SafetyHzControlsImage = options.SafetyHzControlsImage;
            shSet.SafetyHzControlsText = options.SafetyHzControlsText;

            var shSetCount = await _safetyHazardSetService.AllQuery().CountAsync();
            shSet.SafetyHzAbatementNumber = shSetCount;
            shSet.SafetyHzControlsNumber = shSetCount;

            var validationResult = await _safetyHazardSetService.AddAsync(shSet);
            if (validationResult.IsValid)
            {
                var sh = await _shService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
                sh.LinkSafetyHazardSet(shSet);
                await _shService.UpdateAsync(sh);
                return shSet;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<SafetyHazard_Set> UpdateSafetyHazardSet(int id, SafetyHazard_SetCreateOptions options)
        {
            var shSet = await _safetyHazardSetService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, shSet, SafetyHazard_SetOperations.Update);
            if (result.Succeeded)
            {
                // TODO Change Logic as Needed
                shSet.SafetyHzAbatementText = options.SafetyHzAbatementText;
                shSet.SafetyHzControlsText = options.SafetyHzControlsText;
                var validationResult = await _safetyHazardSetService.UpdateAsync(shSet);
                if (validationResult.IsValid)
                {
                    return shSet;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"].Value);
            }
        }
    }
}
