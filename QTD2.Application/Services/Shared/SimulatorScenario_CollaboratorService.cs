using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISimulatorScenarioCollaboratorDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_CollaboratorService;

namespace QTD2.Application.Services.Shared
{
    public class SimulatorScenario_CollaboratorService : ISimulatorScenario_CollaboratorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SimulatorScenario_CollaboratorService> _localizer;
        private readonly ISimulatorScenarioCollaboratorDomainService _simulatorScenarioCollaboratorDomainService;

        public SimulatorScenario_CollaboratorService(ISimulatorScenarioCollaboratorDomainService simulatorScenarioCollaboratorDomainService, IHttpContextAccessor httpContextAccessor,
             IAuthorizationService authorizationService, IStringLocalizer<SimulatorScenario_CollaboratorService> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _simulatorScenarioCollaboratorDomainService = simulatorScenarioCollaboratorDomainService;
        }

        public async Task<SimulatorScenario_Collaborator> GetAsync(int id)
        {
            var simulatorScenario_Collaborator = (await _simulatorScenarioCollaboratorDomainService.FindWithIncludeAsync(x => x.Id == id, new[] { "User", "Permission" })).FirstOrDefault();
            if (simulatorScenario_Collaborator != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, simulatorScenario_Collaborator, SimulatorScenario_CollaboratorOperations.Read);
                if (result.Succeeded)
                {
                    return simulatorScenario_Collaborator;
                }
                else
                {
                    throw new System.UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return simulatorScenario_Collaborator;
        }
    }
}
