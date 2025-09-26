using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SimulatorScenario;
using QTD2.Infrastructure.Model.SimulatorScenario_EnablingObjectives_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioILA_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioPositon_Link;
using QTD2.Infrastructure.Model.SimulatorScenarioTaskObjectives_Link;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISimulatorScenarioService
    {
        //new services
        public Task<SimulatorScenario_VM> CreateAsync(SimulatorScenario_VM options);
        public Task<SimulatorScenario_VM> UpdateAsync(int id, SimulatorScenario_VM options);
        public Task<SimulatorScenarioOverview_VM> GetOverviewAsync();
        public Task<SimulatorScenario_VM> GetAsync(int id);
        public System.Threading.Tasks.Task<object> CopyAsync(int id);
        public Task<List<SimulatorScenario_Position_VM>> UpdatePositionsAsync(int id, SimulatorScenario_UpdatePositions_VM options);
        public Task<SimulatorScenario_TasksResponseVM> UpdateTaskAsync(int id, SimulatorScenario_UpdateTasks_VM options);
        public  System.Threading.Tasks.Task ActiveAsync(int id);
        public  System.Threading.Tasks.Task InActiveAsync(int id);
        public System.Threading.Tasks.Task DeleteSimScenarioById(int simScenarioId);
        public Task<List<SimulatorScenario_EnablingObjective_VM>> UpdateEnablingObjectivesAsync(int id, SimulatorScenario_UpdateEnablingObjectives_VM options);
        public Task<List<SimulatorScenario_Procedure_VM>> UpdateProceduresAsync(int id, SimulatorScenario_UpdateProcedures_VM options);
        public Task<List<SimulatorScenario_Task_Criteria_By_Position_VM>> GetTaskCriteriasForPositionAsync(int id, int positionId);
        public Task<List<SimulatorScenario_Task_Criteria_By_Position_VM>> GetAllTaskCriteriasForPositionAsync(int id);
        public Task<SimulatorScenario_Task_Criteria_VM> CreateTaskCriteriaAsync(int id, SimulatorScenario_Task_Criteria_VM options);
        public Task<SimulatorScenario_Task_Criteria_VM> UpdateTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId, SimulatorScenario_Task_Criteria_VM options);
        public System.Threading.Tasks.Task DeleteTaskCriteriaAsync(int id, int simulatorScenarioTaskCriteriaId);
        public Task<SimulatorScenario_Event_VM> CreateEventAsync(int id, SimulatorScenario_Event_VM options);
        public Task<SimulatorScenario_Event_VM> GetEventAsync(int id, int EventId);
        public System.Threading.Tasks.Task<SimulatorScenario_Event_VM> CopyEventAsync(int id, int eventId);
        public System.Threading.Tasks.Task DeleteEventAsync(int id, int EventId);
        public Task<SimulatorScenario_Event_VM> UpdateEventAsync(int id, int EventId, SimulatorScenario_Event_VM options);
        public System.Threading.Tasks.Task UpdateEventsOrderAsync(int id, SimulatorScenario_UpdateEventsAndScriptsOrder_VM options);
        public Task<List<SimulatorScenario_ILA_VM>> UpdateILAsAsync(int id, SimulatorScenario_UpdateILAs_VM options);
        public Task<List<SimulatorScenario_Prerequisite_VM>> UpdatePrerequisitesAsync(int id, SimulatorScenario_UpdatePrerequisites_VM options);
        public Task<List<SimulatorScenario_Collaborator_VM>> UpdateCollaboratorsAsync(int id, SimulatorScenario_UpdateCollaborators_VM options);
        public System.Threading.Tasks.Task PublishAsync(int id, SimulatorScenario_VM options);
        
        //old services

        public Task<SimulatorScenario> LinkSimulatorScenarioILA(int id, SimulatorScenarioILA_LinkOptions options);

        public System.Threading.Tasks.Task UnLinkSimulatorScenarioILA(int id, SimulatorScenarioILA_LinkOptions options);

        public Task<List<SimulatorScenario>> GetLinkedSimulatorScenarioILAAsync(int id);

        public Task<SimulatorScenario> LinkSimulatorScenarioPosititon(int id, SimulatorScenarioPositon_LinkOptions options);

        public System.Threading.Tasks.Task UnLinkSimulatorScenarioPosition(int id, SimulatorScenarioPositon_LinkOptions options);

        public Task<List<Position>> GetLinkedSimulatorScenarioPositionAsync(int id);

        public Task<SimulatorScenario> LinkSimulatorScenarioEO(int id, SimulatorScenario_EnablingObjectives_LinkOptions options);

        public System.Threading.Tasks.Task UnLinkSimulatorScenarioEO(int id, SimulatorScenario_EnablingObjectives_LinkOptions options);

        public Task<List<EnablingObjective>> GetLinkedSimulatorScenarioEOAsync(int id);

        public Task<SimulatorScenario> LinkSimulatorScenarioTask(int id, SimulatorScenarioTaskObjectives_LinkOptions options);

        public System.Threading.Tasks.Task UnLinkSimulatorScenarioTask(int id, SimulatorScenarioTaskObjectives_LinkOptions options);

        public Task<List<Domain.Entities.Core.Task>> GetLinkedSimulatorScenarioTaskAsync(int id);
        
        public Task<SimulatorScenario_Script_VM> CreateScriptAsync(SimulatorScenario_Script_VM options);
        
        public Task<SimulatorScenario_Script_VM> GetScriptAsync(int scriptId, int eventId);
        
        public Task<SimulatorScenario_Script_VM> CopyScriptAsync(int scriptId , int eventId);
        
        public System.Threading.Tasks.Task DeleteScriptAsync(int scriptId);
        
        public Task<SimulatorScenario_Script_VM> UpdateScriptAsync(int scriptId, int eventId, SimulatorScenario_Script_VM options);
        
        public Task<List<SimulatorScenario_Script>> GetAllScriptAsync();
        
        public Task<List<SimulatorScenario_Position_VM>> GetSimulatorScenario_PositionsAsync(int id);
    }
}
