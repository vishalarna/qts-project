using MediatR;
using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTD2.Application.EventHandlers.Core
{
    public class OnSimulatorScenario_Prerequisites_UpdatedHandler : INotificationHandler<OnSimulatorScenario_Prerequisites_Updated>
    {
        private readonly Domain.Interfaces.Service.Core.ISimulatorScenarioService _simulatorScenarioService;
        private readonly Domain.Interfaces.Service.Core.IILAService _ilaService;
        public OnSimulatorScenario_Prerequisites_UpdatedHandler(Domain.Interfaces.Service.Core.ISimulatorScenarioService simulatorScenarioService,
        Domain.Interfaces.Service.Core.IILAService ilaService
        )
        {
            _simulatorScenarioService = simulatorScenarioService;
            _ilaService = ilaService;
        }
        public async Task Handle(OnSimulatorScenario_Prerequisites_Updated notification, CancellationToken cancellationToken)
        {
            var simulatorScenario = await _simulatorScenarioService.GetWithIncludeAsync(notification.SimulatorScenario.Id, new string[] { "ILAs", "Prerequisites.Prerequisite" });

            if (simulatorScenario != null && simulatorScenario.StatusId == 2)
            {
                var simScenariosILAs = simulatorScenario.ILAs;
                var simScenariosPrerequisites = simulatorScenario.Prerequisites;
                if (simScenariosILAs.Any() && simScenariosPrerequisites.Any())
                {
                    foreach (var simScenariosILA in simScenariosILAs)
                    {
                        var ila = await _ilaService.GetWithIncludeAsync(simScenariosILA.ILAID, new string[] { "ILA_PreRequisite_Links" });
                        var newILAPreRequisiteLinks = simScenariosPrerequisites.Where(x => !ila.ILA_PreRequisite_Links.Any(y => y.PreRequisiteId == x.PrerequisiteId)).ToList();
                        newILAPreRequisiteLinks.ForEach(x => ila.LinkPreRequisite(x.Prerequisite));
                        await _ilaService.UpdateAsync(ila);
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
