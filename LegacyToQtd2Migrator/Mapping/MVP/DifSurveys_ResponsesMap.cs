using QTD2.Domain.Entities.Core;
using LegacyToQtd2Migrator.Legacy.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class DifSurveys_ResponsesMap : Common.MigrationMap<TblDifsurveyEmployee, DIFSurvey_Employee_Response>
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
        List<DIFSurvey> _targetDifSurveys;

        public DifSurveys_ResponsesMap(DbContext source, DbContext target) : base(source, target)
        {

        }
        protected override List<TblDifsurveyEmployee> getSourceRecords()
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
            _targetDifSurveys = (_target as QTD2.Data.QTDContext).DIFSurvey
                    .Include("Employees.Employee.Person")
                    .Include("Tasks.Task.SubdutyArea.DutyArea").ToList();

            return _tblDifsurveyEmployee;
        }

        protected override DIFSurvey_Employee_Response mapRecord(TblDifsurveyEmployee obj)
        {
            var sourceTask = _sourceTasks.Where(r => r.Tid == obj.Tid).First();
            var sourceDutyArea = sourceTask.Da;
            var targetTask = _targetDutyAreas.Where(r => r.Number == sourceDutyArea.Danum).Where(r => r.Letter == sourceDutyArea.Daletter).First().SubdutyAreas.Where(r => r.SubNumber == sourceDutyArea.DasubNum).First().Tasks.Where(r => r.Number == sourceTask.Tnum).First();

            var sourceEmployee = _sourceEmployees.Where(r => r.Eid == obj.Eid).FirstOrDefault();
            var targetEmployee = sourceEmployee == null ? null : _targetEmployees.Where(r => r.EmployeeNumber == sourceEmployee.Enum && r.Person.FirstName == sourceEmployee.EfirstName && r.Person.LastName == sourceEmployee.ElastName).First();

            var targetDifSurvey = _targetDifSurveys
                    .Where(r => r.Title == obj.Difprj.Difprjtitle)
                    .Where(r => r.StartDate == obj.Difprj.StartDate).FirstOrDefault();

            if (targetDifSurvey == null) return null;

            var targetDifSurveyEmployee = targetDifSurvey.Employees.Where(r => r.EmployeeId == targetEmployee.Id).FirstOrDefault();

            if (targetDifSurveyEmployee == null) return null;

            var targetDifSurveyTask = targetDifSurvey.Tasks.Where(r => r.TaskId == targetTask.Id).FirstOrDefault();

            if(targetDifSurveyTask == null)
            {
                targetDifSurveyTask = new DIFSurvey_Task()
                {
                    Active = true,
                    DifSurveyId = targetDifSurvey.Id,
                    CommentingEmployeeId = targetEmployee == null ? null : targetEmployee.Id,
                    DIFSurvey_Task_TrainingFrequencyId = 1,
                    TaskId = targetTask.Id,
                    TrainingStatus_CalculatedId = 1,
                    TrainingStatus_OverrideId = null
                };
            }

            if(targetDifSurveyTask.Id == 0)
            {
                return new DIFSurvey_Employee_Response()
                {
                    Active = true,
                    NA = obj.Na.GetValueOrDefault(),
                    Comments = obj.Comments,
                    Deleted = false,
                    Difficulty = (float?)obj.Difficulty,
                    DIFSurvey_EmployeeId = targetDifSurveyEmployee.Id,
                    DIFSurvey_Task = targetDifSurveyTask,
                    Frequency = (float?)obj.Frequency,
                    Importance = (float?)obj.Importance
                };
            }
            else
            {
                return new DIFSurvey_Employee_Response()
                {
                    Active = true,
                    NA = obj.Na.GetValueOrDefault(),
                    Comments = obj.Comments,
                    Deleted = false,
                    Difficulty = (float?)obj.Difficulty,
                    DIFSurvey_EmployeeId = targetDifSurveyEmployee.Id,
                    DIFSurvey_TaskId = targetDifSurveyTask.Id,
                    Frequency = (float?)obj.Frequency,
                    Importance = (float?)obj.Importance
                };
            }          
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _difSurveys.Count();
        }

        protected override void updateTarget(DIFSurvey_Employee_Response record)
        {
            if (record == null) return;

            (_target as QTD2.Data.QTDContext).DIFSurvey_Employee_Response.Add(record);
        }
    }
}
