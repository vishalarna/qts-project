using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SimulatorScenario_EventService : Common.Service<SimulatorScenario_Event>, ISimulatorScenario_EventService
    {
        public SimulatorScenario_EventService(ISimulatorScenario_EventRepository repository, ISimulatorScenario_EventValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<SimulatorScenario_Event> GetEventByIdAsync(int id)
        {
            var simulatorScenario_Event = await FindAsync(r => r.Id == id);
            return simulatorScenario_Event.FirstOrDefault();
        }

        public async Task<SimulatorScenario_Event> GetScriptByIdAsync(int id)
        {
            List<Expression<Func<SimulatorScenario_Event, bool>>> predicates = new List<Expression<Func<SimulatorScenario_Event, bool>>>();
            predicates.Add(x => x.Id == id);
            var events = await FindWithIncludeAsync(predicates, new[] { "Scripts.Criterias" });
            return events.FirstOrDefault();
        }
    }
}
