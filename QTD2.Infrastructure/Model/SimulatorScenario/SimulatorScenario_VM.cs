using QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_VM
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }
        public int? DifficultyId { get; set; }
        public List<SimulatorScenario_Position_VM> Positions { get; set; } = new List<SimulatorScenario_Position_VM>();
        public List<SimulatorScenario_Task_VM> Tasks { get; set; } = new List<SimulatorScenario_Task_VM>();
        public List<SimulatorScenario_EnablingObjective_VM> EnablingObjectives { get; set; } = new List<SimulatorScenario_EnablingObjective_VM>();
        public List<SimulatorScenario_Procedure_VM> Procedures { get; set; } = new List<SimulatorScenario_Procedure_VM>();
        public List<SimulatorScenario_Task_Criteria_VM> TaskCriterias { get; set; } = new List<SimulatorScenario_Task_Criteria_VM>();
        public string? NetworkConfiguration { get; set; }
        public string? LoadingConditions { get; set; }
        public string? Generation { get; set; }
        public string? Interchange { get; set; }
        public string? OtherBaseCase { get; set; }
        public string? ValidityChecks { get; set; }
        public string? RolePlays { get; set; }
        public string? Documentation { get; set; }
        public List<SimulatorScenario_SimulatorScenarioEventAndScript_VM> EventsAndScripts { get; set; } = new List<SimulatorScenario_SimulatorScenarioEventAndScript_VM>();
        public int? RatingScaleId { get; set; }
        public string? OperatingSkillsEvaluationMethod { get; set; }
        public string? Notes { get; set; }
        public bool MakeAvailableForAllILAs { get; set; }
        public List<SimulatorScenario_ILA_VM> ILAs { get; set; } = new List<SimulatorScenario_ILA_VM>();
        public List<SimulatorScenario_Prerequisite_VM> Prerequisites { get; set; } = new List<SimulatorScenario_Prerequisite_VM>();
        public List<SimulatorScenario_Collaborator_VM> Collaborators { get; set; } = new List<SimulatorScenario_Collaborator_VM>();
        public string? Message { get; set; }
        public DateOnly? PublishedDate { get; set; }
        public string? PublishedReason { get; set; }
        public SimulatorScenario_CollaboratorPermissions_VM CurrentUserPermissions { get; set; }
        public SimulatorScenario_VM(int? id, string title, string? description, int? durationHours, int? durationMinutes, int? difficultyId, string? networkConfiguration, string? loadingConditions,
            string? generation, string? interchange, string? otherBaseCase, string? validityChecks, string? rolePlays, string? documentation, int? ratingScale, string? operatingSkillsEvaluationMethod, string? notes, bool makeAvailableForAllILAs, string? message, DateOnly? publishedDate, string? publishedReason)
        {
            Id = id;
            Title = title;
            Description = description;
            DurationHours = durationHours;
            DurationMinutes = durationMinutes;
            DifficultyId = difficultyId;
            NetworkConfiguration = networkConfiguration;
            LoadingConditions = loadingConditions;
            Generation = generation;
            Interchange = interchange;
            OtherBaseCase = otherBaseCase;
            ValidityChecks = validityChecks;
            RolePlays = rolePlays;
            Documentation = documentation;
            RatingScaleId = ratingScale;
            OperatingSkillsEvaluationMethod = operatingSkillsEvaluationMethod;
            Notes = notes;
            MakeAvailableForAllILAs = makeAvailableForAllILAs;
            Message = message;
            PublishedDate = publishedDate;
            PublishedReason = publishedReason;
        }
    }
}
