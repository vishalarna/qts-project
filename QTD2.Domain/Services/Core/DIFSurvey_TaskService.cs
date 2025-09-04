using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class DIFSurvey_TaskService : Common.Service<DIFSurvey_Task>, IDIFSurvey_TaskService
    {
        public DIFSurvey_TaskService(IDIFSurvey_TaskRepository repository, IDIFSurvey_TaskValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<DIFSurvey_Task>> GetDifSurveyTasksBySurveyIdAsync(int difSurveyId)
        {
            var difSurveyTasks = await FindWithIncludeAsync(x=>x.DifSurveyId == difSurveyId, new string[] { "Task.SubdutyArea.DutyArea" });
            return difSurveyTasks.ToList();
        }

    }
}