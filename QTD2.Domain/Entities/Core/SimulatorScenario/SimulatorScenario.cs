using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario : Entity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }
        public string? NetworkConfiguration { get; set; }
        public string? LoadingConditions { get; set; }
        public string? Generation { get; set; }
        public string? Interchange { get; set; }
        public string? OtherBaseCase { get; set; }
        public string? ValidityChecks { get; set; }
        public string? RolePlays { get; set; }
        public string? Documentation { get; set; }
        public string? OperatingSkillsEvaluationMethod { get; set; }
        public string? Notes { get; set; }
        public string? Message { get; set; }
        public int? RatingScaleId { get; set; }
        public int? DifficultyId { get; set; }
        public DateOnly? PublishedDate { get; set; }
        public string? PublishedReason { get; set; }
        public int? StatusId { get; set; }
        public virtual SimulatorScenario_Difficulty Difficulty { get; set; }
        public virtual SimulatorScenario_Status Status { get; set; }
        public virtual RatingScaleN RatingScale { get; set; }
        public virtual ICollection<SimulatorScenario_ILA> ILAs { get; set; } = new List<SimulatorScenario_ILA>();
        public virtual ICollection<SimulatorScenario_Procedure> Procedures { get; set; } = new List<SimulatorScenario_Procedure>();
        public virtual ICollection<SimulatorScenario_Task_Criteria> TaskCriterias { get; set; } = new List<SimulatorScenario_Task_Criteria>();
        public virtual ICollection<SimulatorScenario_EventAndScript> EventsAndScritps { get; set; } = new List<SimulatorScenario_EventAndScript>();
        public virtual ICollection<SimulatorScenario_Position> Positions { get; set; } = new List<SimulatorScenario_Position>();
        public virtual ICollection<SimulatorScenario_Prerequisite> Prerequisites { get; set; } = new List<SimulatorScenario_Prerequisite>();
        public virtual ICollection<SimulatorScenario_Collaborator> Collaborators { get; set; } = new List<SimulatorScenario_Collaborator>();
        public virtual ICollection<SimulatorScenario_EnablingObjective> EnablingObjectives { get; set; } = new List<SimulatorScenario_EnablingObjective>();
        public virtual ICollection<SimulatorScenario_Task> Tasks { get; set; } = new List<SimulatorScenario_Task>();
        public SimulatorScenario()
        {
        }

        public SimulatorScenario(int? dfficultyId, string title, string description, int? durationHours, int? durationMins, int? statusId)
        {
            DifficultyId = dfficultyId;
            Title = title;
            Description = description;
            DurationHours = durationHours;
            DurationMinutes = durationMins;
            StatusId = statusId;
        }

        public SimulatorScenario_Position UpdatePositions(Position position)
        {
            SimulatorScenario_Position simPosition = Positions.FirstOrDefault(x => x.PositionID == position.Id);
            if (simPosition != null)
            {
                return simPosition;
            }

            simPosition = new SimulatorScenario_Position(this, position);
            AddEntityToNavigationProperty<SimulatorScenario_Position>(simPosition);
            return simPosition;
        }

        public SimulatorScenario_Task UpdateTasks(Task task)
        {
            SimulatorScenario_Task SimTask = Tasks.FirstOrDefault(x => x.TaskId == task.Id);
            if (SimTask != null)
            {
                return SimTask;
            }

            SimTask = new SimulatorScenario_Task(this, task);
            AddEntityToNavigationProperty<SimulatorScenario_Task>(SimTask);
            return SimTask;
        }

        public SimulatorScenario_EnablingObjective UpdateEnablingObjectives(EnablingObjective enablingObjective)
        {
            SimulatorScenario_EnablingObjective SimEnablingObjectives = EnablingObjectives.FirstOrDefault(x => x.EnablingObjectiveID == enablingObjective.Id);
            if (SimEnablingObjectives != null)
            {
                return SimEnablingObjectives;
            }

            SimEnablingObjectives = new SimulatorScenario_EnablingObjective(this, enablingObjective);
            AddEntityToNavigationProperty<SimulatorScenario_EnablingObjective>(SimEnablingObjectives);
            return SimEnablingObjectives;
        }

        public SimulatorScenario_Procedure UpdateProcedures(Procedure procedure)
        {
            SimulatorScenario_Procedure SimProcedures = Procedures.FirstOrDefault(x => x.ProcedureId == procedure.Id);
            if (SimProcedures != null)
            {
                return SimProcedures;
            }

            SimProcedures = new SimulatorScenario_Procedure(this, procedure);
            AddEntityToNavigationProperty<SimulatorScenario_Procedure>(SimProcedures);
            return SimProcedures;
        }

        public void UpdateEventsAndScripts()
        {

        }
        public SimulatorScenario_ILA UpdateILAs(ILA ila)
        {
            SimulatorScenario_ILA simulatorScenario_ILA = ILAs.FirstOrDefault(x => x.ILAID == ila.Id);
            if (simulatorScenario_ILA != null)
            {
                return simulatorScenario_ILA;
            }

            simulatorScenario_ILA = new SimulatorScenario_ILA(ila, this);
            AddEntityToNavigationProperty<SimulatorScenario_ILA>(simulatorScenario_ILA);
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_ILAs_Updated(this));
            return simulatorScenario_ILA;
        }
        public SimulatorScenario_Prerequisite UpdatePrerequisites(ILA prequisiste)
        {
            SimulatorScenario_Prerequisite simulatorScenario_Prerequisite = Prerequisites.FirstOrDefault(x => x.PrerequisiteId == prequisiste.Id);
            if (simulatorScenario_Prerequisite != null)
            {
                return simulatorScenario_Prerequisite;
            }

            simulatorScenario_Prerequisite = new SimulatorScenario_Prerequisite(this, prequisiste);
            AddEntityToNavigationProperty<SimulatorScenario_Prerequisite>(simulatorScenario_Prerequisite);
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Prerequisites_Updated(this));
            return simulatorScenario_Prerequisite;
        }

        public SimulatorScenario_Collaborator UpdateCollaborators(QTDUser collaborator,SimulatorScenario_CollaboratorPermission permission)
        {
            SimulatorScenario_Collaborator simulatorScenario_Collaborator = Collaborators.FirstOrDefault(x => x.UserId == collaborator.Id);
            if (simulatorScenario_Collaborator != null)
            {
                return simulatorScenario_Collaborator;
            }

            simulatorScenario_Collaborator = new SimulatorScenario_Collaborator(this,collaborator, permission);
            AddEntityToNavigationProperty<SimulatorScenario_Collaborator>(simulatorScenario_Collaborator);
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Collaborators_Updated(this));
            return simulatorScenario_Collaborator;
        }
        public void Publish(DateOnly? publishedDate, string publishedReason)
        {
            StatusId = 2;
            PublishedDate = publishedDate;
            PublishedReason = publishedReason;
            AddDomainEvent(new Domain.Events.Core.OnSimulatorScenario_Published(this));
        }

        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as SimulatorScenario;

            copy.Title = "Copy - " + this.Title;
            copy.PublishedDate = null;
            copy.PublishedReason = null;
            copy.StatusId = 1;

            foreach (var ila in ILAs)
            {
                var simScenarioILA = ila.Copy<SimulatorScenario_ILA>(createdBy);
                simScenarioILA.Id = 0;
                copy.ILAs.Add(simScenarioILA);
            }
            foreach (var procedure in Procedures)
            {
                var simScenarioProcedures = procedure.Copy<SimulatorScenario_Procedure>(createdBy);
                simScenarioProcedures.Id = 0;
                copy.Procedures.Add(simScenarioProcedures);
            }
            foreach (var task_Criteria in TaskCriterias)
            {
                var simScenario_TaskCriteria = task_Criteria.Copy<SimulatorScenario_Task_Criteria>(createdBy);
                simScenario_TaskCriteria.Id = 0;
                copy.TaskCriterias.Add(simScenario_TaskCriteria);
            }
            foreach (var eventAndScript in EventsAndScritps)
            {
                var simScenario_EventAndScript = eventAndScript.Copy<SimulatorScenario_EventAndScript>(createdBy);
                simScenario_EventAndScript.Id = 0;
                copy.EventsAndScritps.Add(simScenario_EventAndScript);
            }
            foreach (var position in Positions)
            {
                var simScenario_Position = position.Copy<SimulatorScenario_Position>(createdBy);
                simScenario_Position.Id = 0;
                copy.Positions.Add(simScenario_Position);
            }
            foreach (var prerequisite in Prerequisites)
            {
                var simScenarioPrerequisite = prerequisite.Copy<SimulatorScenario_Prerequisite>(createdBy);
                simScenarioPrerequisite.Id = 0;
                copy.Prerequisites.Add(simScenarioPrerequisite);
            }
            foreach (var collaborator in Collaborators)
            {
                var simScenarioCollaborator = collaborator.Copy<SimulatorScenario_Collaborator>(createdBy);
                simScenarioCollaborator.Id = 0;
                copy.Collaborators.Add(simScenarioCollaborator);
            }
            foreach (var enablingObjective in EnablingObjectives)
            {
                var simScenarioEnablingObjective = enablingObjective.Copy<SimulatorScenario_EnablingObjective>(createdBy);
                simScenarioEnablingObjective.Id = 0;
                copy.EnablingObjectives.Add(simScenarioEnablingObjective);
            }
            foreach (var task in Tasks)
            {
                var simulatorScenario_Task = task.Copy<SimulatorScenario_Task>(createdBy);
                simulatorScenario_Task.Id = 0;
                copy.Tasks.Add(simulatorScenario_Task);
            }
            return (T)(object)copy;
        }
    }
}
