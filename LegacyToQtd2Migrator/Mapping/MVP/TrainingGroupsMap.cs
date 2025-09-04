using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TrainingGroupsMap : Common.MigrationMap<TblTrainingPhase, TrainingGroup>
    {
        List<TblTrainingPhase> _trainingPhases;
        List<RstblPositionsTask> _positionTasks;

        List<TrainingGroup_Category> trainingGroup_Categories = new List<TrainingGroup_Category>();

        public TrainingGroupsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTrainingPhase> getSourceRecords()
        {
            _trainingPhases = (_source as EMP_DemoContext).TblTrainingPhases.ToList();
            _positionTasks = (_source as EMP_DemoContext).RstblPositionsTasks.ToList();
            return _trainingPhases;
        }

        protected override TrainingGroup mapRecord(TblTrainingPhase obj)
        {
            return new TrainingGroup()
            {
                TrainingGroup_Category = getCategory(obj),
                Active = true,
                GroupDescription = obj.Tpdesc,
                GroupName = obj.Tpid + " - " + obj.Tpdesc,
                GroupNumber = obj.Tpnum.GetValueOrDefault(),
                Task_TrainingGroups = getTaskTrainingGroups(obj)
            };
        }

        private TrainingGroup_Category getCategory(TblTrainingPhase obj)
        {
            var position = obj.PidNavigation.Pdesc;
            var targetCategory = trainingGroup_Categories.Where(r => r.Title == position).FirstOrDefault();

            if (targetCategory == null)
            {
                targetCategory = new TrainingGroup_Category()
                {
                    Active = true,
                    Title = position,
                    Description = "From Migration"
                };

                trainingGroup_Categories.Add(targetCategory);

                return targetCategory;
            }

            else return targetCategory;
        }

        private ICollection<Task_TrainingGroup> getTaskTrainingGroups(TblTrainingPhase obj)
        {
            var tasks = _positionTasks.Where(r => r.Tpid == obj.Tpid);
            List<Task_TrainingGroup> trainingGroups = new List<Task_TrainingGroup>();

            var usedTasks = new List<Task>();

            foreach (var task in tasks)
            {
                var sourceTask = task.TidNavigation;
                var sourceDutyArea = task.TidNavigation.Da;

                var targetTask = (_target as QTD2.Data.QTDContext)
                                        .DutyAreas
                                        .Where(r => r.Number == sourceDutyArea.Danum)
                                        .Where(r => r.Letter == sourceDutyArea.Daletter)
                                        .First()
                                        .SubdutyAreas
                                        .Where(r => r.SubNumber == sourceDutyArea.DasubNum)
                                        .First()
                                        .Tasks
                                        .Where(r => r.Number == sourceTask.Tnum).First();

                if (usedTasks.Where(r => r.Id == targetTask.Id).Any()) continue;

                usedTasks.Add(targetTask);

                trainingGroups.Add(new Task_TrainingGroup()
                {
                    TaskId = targetTask.Id,
                    Active = true
                });
            }

            return trainingGroups;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _trainingPhases.Count();
        }

        protected override void updateTarget(TrainingGroup record)
        {
            (_target as QTD2.Data.QTDContext).TrainingGroups.Add(record);
        }
    }
}
