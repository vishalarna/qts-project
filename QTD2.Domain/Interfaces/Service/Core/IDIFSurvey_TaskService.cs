using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDIFSurvey_TaskService : Common.IService<DIFSurvey_Task>
    {
        public Task<List<DIFSurvey_Task>> GetDifSurveyTasksBySurveyIdAsync(int difSurveyId);

    }
}
