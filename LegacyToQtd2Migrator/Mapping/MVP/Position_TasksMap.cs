using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class Position_TasksMap : Common.MigrationMap<RstblPositionsTask, Position_Task>
    {
        List<RstblPositionsTask> _positionTasks;
        List<TblTaskLinkage> _taskLinkages;
        List<TblPosition> _positions;
        List<TblTask> _tasks;


        public Position_TasksMap(DbContext source, DbContext target) : base(source, target)
        {
        }

        protected override List<RstblPositionsTask> getSourceRecords()
        {
            _positionTasks = (_source as EMP_DemoContext).RstblPositionsTasks.ToList();
            _taskLinkages = (_source as EMP_DemoContext).TblTaskLinkages.ToList();

            _positions = (_source as EMP_DemoContext).TblPositions.ToList();
            _tasks = (_source as EMP_DemoContext).TblTasks.ToList();

            return _positionTasks;
        }

        protected override Position_Task mapRecord(RstblPositionsTask obj)
        {
            var sourcePosition = obj.PidNavigation;
            var sourceTask = obj.TidNavigation;
            var sourceDutyArea = obj.TidNavigation.Da;

            var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();
            var targetTask = (_target as QTD2.Data.QTDContext).DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First().SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First().Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            return new Position_Task()
            {
                Active = true,
                PositionId = targetPosition.Id,
                IsR6Impacted = obj.ImpactR6.GetValueOrDefault(),
                //R6ImpactedReason = obj.R6Reason,
                R5ImpactedTasks = getR5ImpactedTasks(obj),
                TaskId = targetTask.Id
            };
        }

        private ICollection<R5ImpactedTask> getR5ImpactedTasks(RstblPositionsTask obj)
        {
            List<R5ImpactedTask> impactedTasks = new List<R5ImpactedTask>();
            var links = _taskLinkages.Where(r => r.Tid == obj.Tid && r.Pid == obj.Pid);

            foreach (var link in links)
            {
                var sourcePosition = _positions.Where(r => r.Pid == link.PidImpacted).First();
                var sourceTask = _tasks.Where(r => r.Tid == link.TidImpacted).FirstOrDefault();

                if (sourceTask == null) continue;

                var sourceDutyArea = sourceTask.Da;

                var targetPosition = (_target as QTD2.Data.QTDContext).Positions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();
                var targetTask = (_target as QTD2.Data.QTDContext)
                  .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                  .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                  .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                impactedTasks.Add(new R5ImpactedTask()
                {
                    Active = true,
                    Deleted = false,
                    ImpactedTaskId = targetTask.Id
                });
            }

            return impactedTasks;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _positionTasks.Count();
        }

        protected override void updateTarget(Position_Task record)
        {
            (_target as QTD2.Data.QTDContext).Position_Tasks.Add(record);
        }

    }
}
