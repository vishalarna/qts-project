using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.SimulatorScenario_Difficulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISimulatorScenario_DifficultyDomainService = QTD2.Domain.Interfaces.Service.Core.ISimulatorScenario_DifficultyService;


namespace QTD2.Application.Services.Shared
{
    public class SimulatorScenarioDifficultyService : ISimulatorScenarioDifficultyService
    {
        private readonly ISimulatorScenario_DifficultyDomainService _simulatorScenario_DifficultyDomainService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SimulatorScenarioDifficultyService> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public SimulatorScenarioDifficultyService(IHttpContextAccessor httpContextAccessor, 
            IAuthorizationService authorizationService, 
            IStringLocalizer<SimulatorScenarioDifficultyService> localizer, 
            ISimulatorScenario_DifficultyDomainService simulatorScenario_DifficultyDomainService, 
            UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _simulatorScenario_DifficultyDomainService = simulatorScenario_DifficultyDomainService;
            _userManager = userManager;
        }

        public async Task<List<SimulatorScenario_Difficulty_VM>> GetAllAsync()
        {
            var simualtorScenarioDifficulties = await _simulatorScenario_DifficultyDomainService.AllAsync();
            if (simualtorScenarioDifficulties != null)
            {
                List<SimulatorScenario_Difficulty_VM> simualtorScenarioDifficulties_VMList = new List<SimulatorScenario_Difficulty_VM>();
                foreach (var simulator in simualtorScenarioDifficulties)
                {
                    var simualtorScenarioDifficulties_VM = new SimulatorScenario_Difficulty_VM(simulator.Id, simulator.Difficulty);
                    simualtorScenarioDifficulties_VMList.Add(simualtorScenarioDifficulties_VM);
                }
                return simualtorScenarioDifficulties_VMList;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }
    }
}
