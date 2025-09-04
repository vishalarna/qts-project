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
using QTD2.Infrastructure.Model.EmpSettingsReleaseType;
using IEmpSettingsReleaseTypeDomainService = QTD2.Domain.Interfaces.Service.Core.IEmpSettingsReleaseTypeService;

namespace QTD2.Application.Services.Shared
{
    public class EmpSettingsReleaseTypeService : IEmpSettingsReleaseTypeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EmpSettingsReleaseTypeService> _localizer;
        private readonly IEmpSettingsReleaseTypeDomainService _empSettingsReleaseTypeDomainService;
        public EmpSettingsReleaseTypeService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<EmpSettingsReleaseTypeService> localizer, IEmpSettingsReleaseTypeDomainService empSettingsReleaseTypeDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _empSettingsReleaseTypeDomainService = empSettingsReleaseTypeDomainService;
        }

        public async Task<List<EmpSettingsReleaseTypeVM>> GetAllAsync()
        {
            List<EmpSettingsReleaseTypeVM> empSettingsReleaseTypeVMs = new List<EmpSettingsReleaseTypeVM>();
            var empTestReleaseDetails = await _empSettingsReleaseTypeDomainService.AllAsync();
            foreach (var empRelease in empTestReleaseDetails)
            {
                empSettingsReleaseTypeVMs.Add(new EmpSettingsReleaseTypeVM(empRelease.Id, empRelease.Type));
            }
            return empSettingsReleaseTypeVMs;
        }

    }
}
