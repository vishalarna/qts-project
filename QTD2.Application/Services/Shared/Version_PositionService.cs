using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IVersion_PositionDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_PositionService;
using IPositionDomainService = QTD2.Domain.Interfaces.Service.Core.IPositionService;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Application.Services.Shared
{
    public class Version_PositionService : Interfaces.Services.Shared.IVersion_PositionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_PositionService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IVersion_PositionDomainService _ver_posService;
        private readonly IPositionDomainService _posService;

        public Version_PositionService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<Version_PositionService> localizer,
            UserManager<AppUser> userManager,
            IVersion_PositionDomainService ver_posService,
            IPositionDomainService posService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _ver_posService = ver_posService;
            _posService = posService;
        }

        public async Task<Version_Position> CreateVersionAsync(Position position)
        {
            var pos = await _posService.FindQuery(x => x.Id == position.Id).FirstOrDefaultAsync();
            if(pos == null)
            {
                throw new BadHttpRequestException(message: _localizer["EONotFound"]);
            }
            else
            {
                var ver_pos = new Version_Position(pos)
                {
                    CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name,
                    CreatedDate = DateTime.Now
                };
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, ver_pos, Version_PositionOperations.Create);
                if (result.Succeeded)
                {
                    var validationResult = await _ver_posService.AddAsync(ver_pos);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                    else
                    {
                        return ver_pos;
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["UnauthorizedOperationException"]);
                }

            }
        }
    }
}
