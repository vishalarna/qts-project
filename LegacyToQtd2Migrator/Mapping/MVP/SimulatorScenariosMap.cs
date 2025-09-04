using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;
using LegacyToQtd2Migrator.Extensions;
using LegacyToQtd2Migrator.Helpers;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class SimulatorScenariosMap : Common.MigrationMap<TblIlaSimulation, SimulatorScenario>
    {
        List<TblIlaSimulation> _ilaSimulationScenario;
        List<TblIlaSimulationObjective> _ilaSimulationObjective;
        List<TblIlaSimulationPosition> _ilaSimulationPosition;
        List<TblIlaSimulationObjective> _simulatorScenarioObjective;
        List<TblIlaSimulationEventGroup> _simulatorEventGroups;
        List<TblIlaSimulationScript> _simulatorScripts;
        List<LktblRatingScale> _sourceRatingScales;
        List<TblPosition> _sourcePositions;
        List<TblCourse> _sourceCourses;
        List<TblTask> _sourceTasks;
        List<TblSkillsKnowledge> _skillsKnowledge;

        List<RatingScaleN> _ratingScales;
        List<Position> _positions;
        List<ILA> _ilas;
        List<DutyArea> _targetDutyAreas;
        List<EnablingObjective> _qtd2EnablingObjectives;

        public SimulatorScenariosMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblIlaSimulation> getSourceRecords()
        {
            _ilaSimulationScenario = (_source as EMP_DemoContext).TblIlaSimulations.ToListAsync().Result;
            _ilaSimulationPosition = (_source as EMP_DemoContext).TblIlaSimulationPositions.ToListAsync().Result;
            _ilaSimulationObjective = (_source as EMP_DemoContext).TblIlaSimulationObjectives.ToListAsync().Result;
            _simulatorScenarioObjective = (_source as EMP_DemoContext).TblIlaSimulationObjectives.ToListAsync().Result;
            _simulatorScripts = (_source as EMP_DemoContext).TblIlaSimulationScripts.ToListAsync().Result;
            _simulatorEventGroups = (_source as EMP_DemoContext).TblIlaSimulationEventGroups.ToListAsync().Result;
            _sourceRatingScales = (_source as EMP_DemoContext).LktblRatingScales.ToListAsync().Result;
            _sourcePositions = (_source as EMP_DemoContext).TblPositions.ToListAsync().Result;
            _sourceCourses = (_source as EMP_DemoContext).TblCourses.ToListAsync().Result;
            _sourceTasks = (_source as EMP_DemoContext).TblTasks.ToList();
            _skillsKnowledge = (_source as EMP_DemoContext).TblSkillsKnowledges.ToList();

            _ratingScales = (_target as QTD2.Data.QTDContext).RatingScaleNs.ToList();
            _positions = (_target as QTD2.Data.QTDContext).Positions.ToList();
            _ilas = (_target as QTD2.Data.QTDContext).ILAs.ToList();
            _targetDutyAreas = (_target as QTD2.Data.QTDContext).DutyAreas.ToList();

            return _ilaSimulationScenario;
        }

        protected override SimulatorScenario mapRecord(TblIlaSimulation obj)
        {
            return new SimulatorScenario()
            {
                Active = true,
                EventsAndScritps = getEventAndScripts(obj),
                ILAs = getIlas(obj),
                Collaborators = getCollaborators(obj),
                Deleted = false,
                Description = obj.Notes,
                DifficultyId = 1,
                Documentation = obj.DocumentationProcedures,
                DurationHours = 0,
                DurationMinutes = 0,
                EnablingObjectives = getEnablingObjectives(obj),
                Generation = obj.Generation,
                Interchange = obj.Interchange,
                LoadingConditions = obj.LoadingConditions,
                Message = obj.ScenarioDesc,
                NetworkConfiguration = obj.NetworkConfiguration,
                Notes = obj.InstructorPrep,
                OperatingSkillsEvaluationMethod = "",
                OtherBaseCase = obj.Other,
                Positions = getPositions(obj),
                Prerequisites = getPreqrequestites(obj),
                Procedures = getProducures(obj),
                PublishedDate = DateOnly.FromDateTime(DateTime.Now),
                PublishedReason = "From Migration",
                RatingScaleId = getRatingScaleId(obj),
                RolePlays = obj.RolePlays,
                StatusId = 1,
                Tasks = getTasks(obj),
                TaskCriterias = getTaskCriterias(obj),
                Title = string.IsNullOrEmpty(obj.ScenarioTitle) ? "" : obj.ScenarioTitle,
                ValidityChecks = obj.ValidityChecks
            };
        }

        private ICollection<SimulatorScenario_Task_Criteria> getTaskCriterias(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Task_Criteria> taskCriterias = new List<SimulatorScenario_Task_Criteria>();

            //unknown legacy location

            //var sourceTaskCriterias = _ilaSimulationObjective.Where(r => r.Ilasimid == obj.IlasimId).Where(r => r.ObjectiveType.ToUpper() == "TASK");

            //foreach(var sourceTaskCriteria in sourceTaskCriterias)
            //{
            //    var sourceTask = _sourceTasks.Where(r => r.Tid == sourceTaskCriteria.ObjectiveId).FirstOrDefault();

            //    if (sourceTask == null) continue;

            //    var sourceDutyArea = sourceTask.Da;

            //    var targetTask = _targetDutyAreas
            //            .Where(r => r.Number == sourceDutyArea.Danum)
            //            .Where(r => r.Letter == sourceDutyArea.Daletter).First()
            //            .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
            //            .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            //    taskCriterias.Add(new SimulatorScenario_Task_Criteria()
            //    {
            //        Active = true,
            //        Deleted = false,
            //        TaskId = targetTask.Id
            //    });
            //}

            return taskCriterias;
        }

        private ICollection<SimulatorScenario_Task> getTasks(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Task> tasks = new List<SimulatorScenario_Task>();

            var sourceTasks = _ilaSimulationObjective.Where(r => r.Ilasimid == obj.IlasimId).Where(r => r.ObjectiveType.ToUpper() == "TASK");

            foreach (var simulationObjective in sourceTasks)
            {
                var sourceTask = _sourceTasks.Where(r => r.Tid == simulationObjective.ObjectiveId).FirstOrDefault();

                if (sourceTask == null) continue;

                var sourceDutyArea = sourceTask.Da;

                var targetTask = _targetDutyAreas
                        .Where(r => r.Number == sourceDutyArea.Danum)
                        .Where(r => r.Letter == sourceDutyArea.Daletter).First()
                        .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                        .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                tasks.Add(new SimulatorScenario_Task()
                {
                    Active = true,
                    Deleted = false,
                    TaskId = targetTask.Id
                });
            }

            return tasks;
        }

        private ICollection<SimulatorScenario_EnablingObjective> getEnablingObjectives(TblIlaSimulation obj)
        {
            List<SimulatorScenario_EnablingObjective> eos = new List<SimulatorScenario_EnablingObjective>();

            var sourceEoCriterias = _ilaSimulationObjective.Where(r => r.Ilasimid == obj.IlasimId).Where(r => r.ObjectiveType.ToUpper() == "S/K");

            foreach (var sourceEoCriteria in sourceEoCriterias)
            {
                var sourceEo = _skillsKnowledge.Where(r => r.Skid == sourceEoCriteria.ObjectiveId).FirstOrDefault();

                if (sourceEo == null) continue;

                var targetEo = _qtd2EnablingObjectives.FindEnablingObjective(sourceEo, sourceEo.CidNavigation, (_target as QTD2.Data.QTDContext));

                eos.Add(new SimulatorScenario_EnablingObjective()
                {
                    Active = true,
                    Deleted = false,
                    EnablingObjectiveID = targetEo.Id
                });
            }

            return eos;
        }

        private int? getRatingScaleId(TblIlaSimulation obj)
        {
            var position = _ilaSimulationPosition.Where(r => r.Ilasimid == obj.IlasimId).FirstOrDefault();

            if (position == null) return null;

            var ratingScale = _sourceRatingScales.Where(r => r.Rsid == position.Rsid).FirstOrDefault();

            if (ratingScale == null) return null;

            var ratingScaleN = _ratingScales.Where(r => r.RatingScaleDescription == ratingScale.Rsdescription).FirstOrDefault();

            if (ratingScaleN == null) return null;

            return ratingScaleN.Id;
        }

        private ICollection<SimulatorScenario_Procedure> getProducures(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Procedure> procedures = new List<SimulatorScenario_Procedure>();

            //unknown legacy location

            return procedures;
        }

        private ICollection<SimulatorScenario_Prerequisite> getPreqrequestites(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Prerequisite> preqrequestites = new List<SimulatorScenario_Prerequisite>();

            //unknown legacy location

            return preqrequestites;
        }

        private ICollection<SimulatorScenario_Position> getPositions(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Position> positions = new List<SimulatorScenario_Position>();

            var sourcePositions = _ilaSimulationPosition.Where(r => r.Ilasimid == obj.IlasimId);

            foreach (var simPosition in sourcePositions)
            {
                var sourcePosition = _sourcePositions.Where(r => r.Pid == simPosition.PosId).FirstOrDefault();

                if (sourcePosition == null) continue;

                var targetPosition = _positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).FirstOrDefault();

                if (targetPosition == null) continue;

                positions.Add(new SimulatorScenario_Position()
                {
                    Active = true,
                    Deleted = false,
                    PositionID = targetPosition.Id
                });
            }

            return positions;
        }

        private ICollection<SimulatorScenario_Collaborator> getCollaborators(TblIlaSimulation obj)
        {
            List<SimulatorScenario_Collaborator> simulatorScenario_Collaborators = new List<SimulatorScenario_Collaborator>();

            //new for 2.0

            return simulatorScenario_Collaborators;
        }

        private ICollection<SimulatorScenario_ILA> getIlas(TblIlaSimulation obj)
        {
            List<SimulatorScenario_ILA> ilas = new List<SimulatorScenario_ILA>();

            var sourceCourse = _sourceCourses.Where(r => r.Corid == obj.Corid).FirstOrDefault();

            if (sourceCourse == null) return ilas;

            var targetIla = (_target as QTD2.Data.QTDContext).ILAs.Where(r => r.Number == sourceCourse.Cornum).First();

            ilas.Add(new SimulatorScenario_ILA()
            {
                Active = true,
                ILAID = targetIla.Id,
                Deleted = false
            });

            return ilas;
        }

        private ICollection<SimulatorScenario_EventAndScript> getEventAndScripts(TblIlaSimulation obj)
        {
            List<SimulatorScenario_EventAndScript> eventsAndScripts = new List<SimulatorScenario_EventAndScript>();

            return eventsAndScripts;

            var sourceScripts = _simulatorScripts.Where(r => r.IlasimId == obj.IlasimId);

            foreach (var sourceScript in sourceScripts)
            {
                var sourcePosition = _sourcePositions.Where(r => r.Pid == sourceScript.InitiatorPosId).FirstOrDefault();
                var targetPosition = _positions.Where(r => r.PositionAbbreviation == sourcePosition?.Pabbrev).FirstOrDefault();

                eventsAndScripts.Add(new SimulatorScenario_EventAndScript()
                {
                    Active = true,
                    Deleted = false,
                    Criterias = getEventAndScriptsCriteria(sourceScript, obj),
                    Description = "",
                    InitiatorId = targetPosition?.Id ?? 1,
                    Order = sourceScripts.ToList().IndexOf(sourceScript) + 1,
                    Title = sourceScript.Event
                });
            }

            var sourceEvents = _simulatorEventGroups.Where(r => r.Ilasimid == obj.IlasimId);

            foreach (var sourceEvent in sourceEvents)
            {
                eventsAndScripts.Add(new SimulatorScenario_EventAndScript()
                {
                    Active = true,
                    Deleted = false,
                    Description = "",
                    Order = sourceScripts.Count() + sourceEvents.ToList().IndexOf(sourceEvent) + 1,
                    Title = sourceEvent.Egdesc
                });
            }

            return eventsAndScripts;
        }

        private List<SimulatorScenario_EventAndScript_Criteria> getEventAndScriptsCriteria(TblIlaSimulationScript sourceScript, TblIlaSimulation obj)
        {
            return new List<SimulatorScenario_EventAndScript_Criteria>();
            //throw new NotImplementedException();
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _ilaSimulationScenario.Count();
        }

        protected override void updateTarget(SimulatorScenario record)
        {
            (_target as QTD2.Data.QTDContext).SimulatorScenarios.Add(record);
        }
    }
}
