using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.SimulatorScenario_Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISimulatorScenario_StatusDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_StatusService;

namespace QTD2.Application.Services.Shared
{
  public  class SimulatorScenarioStatusService : ISimulatorScenarioStatusService
    {
        private readonly ISimulatorScenario_StatusDomainService _simulatorScenario_StatusDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SimulatorScenarioStatusService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public SimulatorScenarioStatusService(IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<SimulatorScenarioStatusService> localizer,
            ISimulatorScenario_StatusDomainService simulatorScenario_StatusDomainService,
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _simulatorScenario_StatusDomainService = simulatorScenario_StatusDomainService;
            _userManager = userManager;
        }

        public async Task<List<SimulatorScenario_Status_VM>> GetAllAsync()
        {
            var simulatorScenarioStatuses = await _simulatorScenario_StatusDomainService.AllAsync();
            if (simulatorScenarioStatuses != null)
            {
                List<SimulatorScenario_Status_VM> simulatorScenarioStatus_VMList = new List<SimulatorScenario_Status_VM>();
                foreach (var status in simulatorScenarioStatuses)
                {
                    var simulatorScenarioStatus_VM = new SimulatorScenario_Status_VM(status.Id, status.Status);
                    simulatorScenarioStatus_VMList.Add(simulatorScenarioStatus_VM);
                }
                return simulatorScenarioStatus_VMList;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }
}
}
