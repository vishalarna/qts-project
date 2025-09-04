using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
   public interface IDIFSurveyTaskTrainingFrequencyService
    {
        public Task<List<DIFSurvey_Task_TrainingFrequency>> GetTaskTrainingFrequencyAsync();
    }
}
