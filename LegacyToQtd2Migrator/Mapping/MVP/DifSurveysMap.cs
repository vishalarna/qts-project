using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class DifSurveysMap : Common.MigrationMap<TblDifproject, DIFSurvey>
    {
        List<TblDifproject> _difSurveys;
        List<TblDifsurveyPosition> _tblDIFSurveyPosition;
        List<TblDifsurveyEmployee> _tblDifsurveyEmployee;
        List<TblDifsurveyEmplSummary> _tblDifsurveyEmplSummary;
        List<TblPosition> _sourcePositions;
        List<TblTask> _sourceTasks;
        List<TblEmployee> _sourceEmployees;

        List<Position> _targetPositions;
        List<DutyArea> _targetDutyAreas;
        List<Employee> _targetEmployees;

        public DifSurveysMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblDifproject> getSourceRecords()
        {
            _difSurveys = (_source as EMP_DemoContext).TblDifprojects.ToList();
            _tblDIFSurveyPosition = (_source as EMP_DemoContext).TblDifsurveyPositions.ToList();
            _tblDifsurveyEmployee = (_source as EMP_DemoContext).TblDifsurveyEmployees.ToList();
            _tblDifsurveyEmplSummary = (_source as EMP_DemoContext).TblDifsurveyEmplSummaries.ToList();

            _sourcePositions = (_source as EMP_DemoContext).TblPositions.ToList();
            _sourceTasks = (_source as EMP_DemoContext).TblTasks.ToList();
            _sourceEmployees = (_source as EMP_DemoContext).TblEmployees.ToList();

            _targetPositions = (_target as QTD2.Data.QTDContext).Positions.ToList();
            _targetDutyAreas = (_target as QTD2.Data.QTDContext).DutyAreas.ToList();
            _targetEmployees = (_target as QTD2.Data.QTDContext).Employees.Include("Person").ToList();

            return _difSurveys;
        }

        protected override DIFSurvey mapRecord(TblDifproject obj)
        {
            var sourcePosition = _sourcePositions.Where(r => r.Pid == obj.Pid).FirstOrDefault();

            if (sourcePosition == null) return null;

            var targetPosition = _targetPositions.Where(r => r.PositionAbbreviation == sourcePosition.Pabbrev).First();

            return new DIFSurvey()
            {
                Active = true,
                Deleted = false,
                DevStatusId = 2,
                DueDate = obj.EndDate,
                Employees = getEmployees(obj),
                HistoricalOnly = obj.HistoricalOnly,
                Instructions = obj.Explanation.Contains(@"{\rtf") ? RtfPipe.Rtf.ToHtml(obj.Explanation ?? "") : obj.Explanation,
                ReleasedToEMP = true,
                StartDate = obj.StartDate,
                Tasks = getTasks(obj),
                Title = obj.Difprjtitle,
                PositionId = targetPosition.Id
            };
        }

        private List<DIFSurvey_Task> getTasks(TblDifproject obj)
        {
            List<DIFSurvey_Task> tasks = new List<DIFSurvey_Task>();

            var sourceDifTasks = _tblDIFSurveyPosition.Where(r => r.Difprjid == obj.Difprjid);

            foreach (var sourceDifTask in sourceDifTasks)
            {
                var sourceTask = _sourceTasks.Where(r => r.Tid == sourceDifTask.Tid).FirstOrDefault();

                if (sourceTask == null) continue;

                var sourceDutyArea = sourceTask.Da;
                var targetTask = _targetDutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First().SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First().Tasks.Where(r => r.Number == sourceTask.Tnum).First();

                var sourceEmployee = _sourceEmployees.Where(r => r.Eid == sourceDifTask.CtEmp.GetValueOrDefault()).FirstOrDefault();
                var targetEmployee = sourceEmployee == null ? null : _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();

                tasks.Add(new DIFSurvey_Task()
                {
                    Active = true,
                    AverageDifficulty = (float?)sourceDifTask.AvgDifficulty,
                    AverageFrequency = (float?)sourceDifTask.AvgFrequency,
                    AverageImportance = (float?)sourceDifTask.AvgImportance,
                    CommentingEmployeeId = targetEmployee == null ? null : targetEmployee.Id,
                    Comments = sourceDifTask.Comments,
                    DIFSurvey_Task_TrainingFrequencyId = 1,
                    TaskId = targetTask.Id,
                    TrainingStatus_CalculatedId = sourceDifTask.StatusDefault.HasValue ? ( sourceDifTask.StatusDefault.Value == 0 ? 4 : sourceDifTask.StatusDefault.Value) : 4,
                    TrainingStatus_OverrideId = sourceDifTask.StatusFinal
                });
            }

            return tasks;
        }

        private List<DIFSurvey_Employee> getEmployees(TblDifproject obj)
        {
            List<DIFSurvey_Employee> employees = new List<DIFSurvey_Employee>();

            var sourceDifEmployees = _tblDifsurveyEmplSummary.Where(r => r.Difprjid == obj.Difprjid);

            foreach (var sourceDifEmployee in sourceDifEmployees)
            {
                var sourceEmployee = _sourceEmployees.Where(r => r.Eid == sourceDifEmployee.Eid).FirstOrDefault();
                var targetEmployee = sourceEmployee == null ? null : _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();

                employees.Add(new DIFSurvey_Employee()
                {
                    Active = true,
                    Comments = sourceDifEmployee.PosComments,
                    Complete = sourceDifEmployee.Complete,
                    CompletedDate = sourceDifEmployee.CompletedDate,
                    Deleted = false,
                    EmployeeId = targetEmployee.Id,
                    ReleaseDate = sourceDifEmployee.ReleasedDate,
                    Started = sourceDifEmployee.Started,
                    StatusId = 1
                });
            }

            return employees;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _difSurveys.Count();
        }

        protected override void updateTarget(DIFSurvey record)
        {
            if (record != null)
                (_target as QTD2.Data.QTDContext).DIFSurvey.Add(record);
        }
    }
}
