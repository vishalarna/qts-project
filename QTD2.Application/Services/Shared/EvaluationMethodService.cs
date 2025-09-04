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
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using IEvaluationMethodDomainService = QTD2.Domain.Interfaces.Service.Core.IEvaluationMethodService;

namespace QTD2.Application.Services.Shared
{
    public class EvaluationMethodService : IEvaluationMethodService
    {
        private readonly IStringLocalizer<EvaluationMethod> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEvaluationMethodDomainService _eval_methodService;

        public EvaluationMethodService(
            IStringLocalizer<EvaluationMethod> localizer,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager,
            IEvaluationMethodDomainService eval_methodService)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _eval_methodService = eval_methodService;
        }

        public async Task<List<EvaluationMethod>> GetAllAsync()
        {
            var evals = await _eval_methodService.AllQuery().ToListAsync();
            return evals;
        }
    }
}
