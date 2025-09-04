using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDIFSurveyService : Common.IService<DIFSurvey>
    {
        public Task<DIFSurvey> GetSurveyByEmpIdAsync(int employeeId, int difSurveyId);
        public System.Threading.Tasks.Task<List<DIFSurvey>> GetAllAsync();
        public Task<List<DIFSurvey>> GetDifSurveyByIdsAsync(List<int> difSurveyId);
        public Task<List<DIFSurvey>> GetDifSurveyFeedbackByIdsAsync(List<int> difSurveyId, List<int> employeeIds);
        public Task<List<DIFSurvey>> GetDifSurveyAggregateResultsAsync(List<int> difSurveyId, string activeStatus, bool includeSeudoTasks,int trainingFrequencyId);
    }
}
