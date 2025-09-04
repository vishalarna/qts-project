using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission;
using QTD2.Infrastructure.Model.SimulatorScenario_Difficulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISimulatorScenario_CollaboratorPermissionDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorPermissionService;

namespace QTD2.Application.Services.Shared
{
    public class SimulatorScenarioCollaboratorPermissonService : ISimulatorScenarioCollaboratorPermissionsService
    {
        private readonly ISimulatorScenario_CollaboratorPermissionDomainService _simulatorScenario_CollaboratorPermissionDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SimulatorScenarioCollaboratorPermissonService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public SimulatorScenarioCollaboratorPermissonService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<SimulatorScenarioCollaboratorPermissonService> localizer,
            ISimulatorScenario_CollaboratorPermissionDomainService simulatorScenario_CollaboratorPermissionDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _simulatorScenario_CollaboratorPermissionDomainService = simulatorScenario_CollaboratorPermissionDomainService;
            _userManager = userManager;
        }

        public async Task<List<SimulatorScenario_CollaboratorPermissions_VM>> GetAllAsync()
        {
            var collaboratorPermissions = await _simulatorScenario_CollaboratorPermissionDomainService.AllAsync();
            if (collaboratorPermissions != null)
            {
                List<SimulatorScenario_CollaboratorPermissions_VM> collaboratorPermissions_VMList = new List<SimulatorScenario_CollaboratorPermissions_VM>();
                foreach (var collaboratorPermission in collaboratorPermissions)
                {
                    var collaboratorPermission_VM = new SimulatorScenario_CollaboratorPermissions_VM(collaboratorPermission.Id, collaboratorPermission.Permission);
                    collaboratorPermissions_VMList.Add(collaboratorPermission_VM);
                }
                return collaboratorPermissions_VMList;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }
    }
}
