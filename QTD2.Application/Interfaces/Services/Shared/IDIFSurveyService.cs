using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.DIFSurvey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IDIFSurveyService
    {
        public Task<DIFSurvey> CreateAsync(DIFSurvey_CreateOptions options);
        public Task<DIFSurveyVM> UpdateAsync(int id, DIFSurvey_UpdateOptions options);
        public Task<DIFSurvey> EditDIFSurveyAsync(int id, string editType);
        public Task<DifSurveyOverview_VM> GetAllAsync();
        public Task<DIFSurveyVM> GetAsync(int id);
        public Task<List<DIFSurveyEmployeeVM>> GetCompletedSurveyByEmpId(int employeeId);
        public Task<List<DIFSurveyEmployeeVM>> GetPendingSurveyByEmpId(int employeeId);
        public Task<DIFSurvey> GetTaskRatingAsync(int id);
        public Task<List<DIFSurveyTaskVM>> LinkTasksToDifSurveyAsync(DIFSurveyTaskLinkOptions options);
        public System.Threading.Tasks.Task UnlinkTaskFromDifSurveyAsync(int difSurveyTaskId);
        public Task<DIFSurvey_Task> UpdateDifTaskResultsAsync(int dsTaskId, DIFResult_UpdateOptions options);
        public Task<List<DIFSurvey_EmployeeVM>> LinkEmployeesToDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options);
        public System.Threading.Tasks.Task UnlinkEmployeesFromDifSurveyAsync(DIFSurveyEmployeeLinkUnlinkOptions options);
        public Task<DIFSurveyVM> GetEnrollmentsBySurveyIdAsync(int id);

        public Task<DifSurveyViewResponseVm> GetEmployeeResponsesByEmpId(int employeeId, int difSurveyId);
        public System.Threading.Tasks.Task CreateUpdateEmployeeResponsesAsync(int difSurveyId,int difEmployeeId,DIFSurveyEmployeeResponseOptions options,bool isCompleted);
    }
}
