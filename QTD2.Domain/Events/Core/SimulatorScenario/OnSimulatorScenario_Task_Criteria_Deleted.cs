using MediatR;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Events.Core
{
    public class OnSimulatorScenario_Task_Criteria_Deleted : Common.IDomainEvent, INotification
    {
        public SimulatorScenario_Task_Criteria SimulatorScenario_Task_Criteria { get; }
        public OnSimulatorScenario_Task_Criteria_Deleted(SimulatorScenario_Task_Criteria simScenario_Task_Criteria)
        {
            SimulatorScenario_Task_Criteria = simScenario_Task_Criteria;
        }
    }

}
