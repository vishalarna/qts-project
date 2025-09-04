using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using System;

using Microsoft.EntityFrameworkCore;
using LegacyToQtd2Migrator.Helpers;


namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class TaskHistoriesMap : Common.MigrationMap<TblTasksHistory, Task_History>
    {
        List<TblTasksHistory> _sourceTaskHistories;
        List<TblPositionTasksHistory> _positionTaskHistories;
        List<TblTaskAuditChangeType> _taskAuditChangeTypes;
        List<TblTasksAuditHistory> _taskAuditHistories;
        List<TblTask> _tasks;

        public TaskHistoriesMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<TblTasksHistory> getSourceRecords()
        {
            _sourceTaskHistories = (_source as EMP_DemoContext).TblTasksHistories.ToList();
            _positionTaskHistories = (_source as EMP_DemoContext).TblPositionTasksHistories.ToList();
            _taskAuditChangeTypes = (_source as EMP_DemoContext).TblTaskAuditChangeTypes.ToList();
            _taskAuditHistories = (_source as EMP_DemoContext).TblTasksAuditHistories.ToList();
            _tasks = (_source as EMP_DemoContext).TblTasks.ToList();

            return _sourceTaskHistories;
        }

        protected override Task_History mapRecord(TblTasksHistory obj)
        {
            var taskAuditHistory = _taskAuditHistories.Where(r => r.Thid == obj.Thid).Where(r => r.ChangeEntity != "Task-Annual-Review").FirstOrDefault();
            var positionTaskHistory = _positionTaskHistories.Where(r => r.Thid == obj.Thid).FirstOrDefault();
            var changeType = taskAuditHistory == null || taskAuditHistory.ChangeType == null ? null : _taskAuditChangeTypes.Where(r => r.Tact == taskAuditHistory.ChangeType).First();

            var sourceTask = _tasks.Where(r => r.Tid == obj.Tid).FirstOrDefault();

            if (sourceTask == null) return null;

            var sourceDutyArea = sourceTask.Da;
            var targetTask = (_target as QTD2.Data.QTDContext)
                .DutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First()
                .SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First()
                .Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            var targetVersionTask = (_target as QTD2.Data.QTDContext)
                                    .Version_Tasks
                                    .Where(r => r.State == obj.Thid)
                                    .First();

            string thProcList = obj.ThprocList ?? "";
            bool setAsThProcList = !string.IsNullOrEmpty(thProcList) && thProcList.Contains("*");
            targetVersionTask.State = 0;

            var taskHistory = new Task_History()
            {
                ChangeEffectiveDate = obj.Thdate.GetValueOrDefault().ToQtsTime(false),
                Active = true,
                ChangeNotes = setAsThProcList ? "Task Procedure List" : changeType == null ? "" : changeType.TaskAuditChangeType,
                NewStatus = Convert.ToBoolean(obj.Inactive.Replace("*", "")),
                OldStatus = true,
                Task = targetVersionTask.Task,
                Version_Task = targetVersionTask,
            };

            return taskHistory;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _sourceTaskHistories.Count();
        }

        protected override void updateTarget(Task_History record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).Task_Histories.Add(record);
        }
    }
}
