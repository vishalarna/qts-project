using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using IRatingScaleNDomainService = QTD2.Domain.Interfaces.Service.Core.IRatingScaleNService;

namespace QTD2.Application.Services.Shared
{
    public class RatingScaleNService : IRatingScaleNService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<RatingScaleNService> _localizer;
        private readonly IRatingScaleNDomainService _ratingScaleService;
        private readonly UserManager<AppUser> _userManager;

        public RatingScaleNService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<RatingScaleNService> localizer, IRatingScaleNDomainService ratingScaleService, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _ratingScaleService = ratingScaleService;
            _userManager = userManager;
        }

        public async Task<List<RatingScaleN>> GetAsync()
        {
            var obj_list = await _ratingScaleService.AllAsync();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list?.ToList();
        }

    }
}
