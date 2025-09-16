using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurveyService : Common.Service<DIFSurvey>, IDIFSurveyService
    {
        public DIFSurveyService(IDIFSurveyRepository repository, IDIFSurveyValidation validation)
            : base(repository, validation)
        {

        }

        public async Task<DIFSurvey> GetSurveyByEmpIdAsync(int employeeId, int difSurveyId)
        {
            List<Expression<Func<DIFSurvey, bool>>> predicates = new List<Expression<Func<DIFSurvey, bool>>>();
            predicates.Add(x => x.Id == difSurveyId);
            predicates.Add(x => x.Employees.Any(e => e.EmployeeId == employeeId));

            var difSurvey = await FindWithIncludeAsync(predicates, new[] { "Employees", "Tasks.Responses" });
            var difSurveyTask = await FindWithIncludeAsync(predicates, new[] { "Tasks.Task.SubdutyArea.DutyArea" });
            if (difSurvey != null)
            {
                difSurvey.FirstOrDefault().Tasks = difSurveyTask.FirstOrDefault().Tasks;
            }
            return difSurvey.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<DIFSurvey>> GetAllAsync()
        {
            var difSurveyList = await AllWithIncludeAsync(new string[] { "Position" });
            return difSurveyList.ToList();
        }

        public async Task<List<DIFSurvey>> GetDifSurveyByIdsAsync(List<int> difSurveyId)
        {
            List<Expression<Func<DIFSurvey, bool>>> predicates = new List<Expression<Func<DIFSurvey, bool>>>();
            predicates.Add(r => difSurveyId.Contains(r.Id));
            var difSurveyList = (await FindWithIncludeAsync(predicates, new string[] { "Position", "Tasks.Task.SubdutyArea.DutyArea" })).ToList();
            return difSurveyList;
        }

        public async Task<List<DIFSurvey>> GetDifSurveyFeedbackByIdsAsync(List<int> difSurveyId, List<int> employeeIds)
        {
            List<Expression<Func<DIFSurvey, bool>>> predicates = new List<Expression<Func<DIFSurvey, bool>>>();
            predicates.Add(r => difSurveyId.Contains(r.Id));
            var difSurveyList = (await FindWithIncludeAsync(predicates, new string[] { "Position", "Employees.Employee.Person", "Employees.Responses", "Employees.Employee.EmployeePositions" })).ToList();
            var difSurveyListWithTasks = (await FindWithIncludeAsync(predicates, new string[] { "Tasks.Task.SubdutyArea.DutyArea" })).ToList();

            foreach(var difSurvey in difSurveyList)
            {
                difSurvey.Tasks = difSurveyListWithTasks.Where(r => r.Id == difSurvey.Id).First().Tasks;
            }
            return difSurveyList;
        }

        public async Task<List<DIFSurvey>> GetDifSurveyAggregateResultsAsync(List<int> difSurveyId, string activeStatus, bool includePseudoTasks, int trainingFrequencyId)
        {
            List<Expression<Func<DIFSurvey, bool>>> predicates = new List<Expression<Func<DIFSurvey, bool>>>();
            predicates.Add(r => difSurveyId.Contains(r.Id));

            var difSurveyList = (await FindWithIncludeAsync(predicates, new string[] { "Position", "Tasks.Task.SubdutyArea.DutyArea", "Tasks.DIFSurvey_Task_TrainingFrequency", "Tasks.TrainingStatus_Calculated", "Tasks.TrainingStatus_Override", "Employees", "Tasks.Responses" })).ToList();
            foreach (var survey in difSurveyList)
            {
                if (activeStatus == "Active Only")
                {
                    survey.Tasks = survey.Tasks.Where(e => e.Task.Active).ToList();
                }
                else if (activeStatus == "Inactive Only")
                {
                    survey.Tasks = survey.Tasks.Where(e => !e.Task.Active).ToList();
                }

                if (trainingFrequencyId != 0)
                {
                    survey.Tasks = survey.Tasks.Where(e => e.DIFSurvey_Task_TrainingFrequencyId == trainingFrequencyId).ToList();
                }

                if (!includePseudoTasks)
                {
                    survey.Tasks = survey.Tasks.Where(e => e.Task.SubdutyArea.DutyArea.Letter != "P").ToList();
                }
            }
            return difSurveyList;
        }
    }
}
