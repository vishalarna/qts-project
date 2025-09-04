using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Services.Shared
{
    public class DIFSurveyTaskTrainingFrequencyService : IDIFSurveyTaskTrainingFrequencyService
    {
        private readonly IDIFSurvey_Task_TrainingFrequencyService _dIFSurvey_Task_TrainingFrequencyService;

        public DIFSurveyTaskTrainingFrequencyService(IDIFSurvey_Task_TrainingFrequencyService dIFSurvey_Task_TrainingFrequencyService)
        {
            _dIFSurvey_Task_TrainingFrequencyService = dIFSurvey_Task_TrainingFrequencyService;
        }
        public async Task<List<DIFSurvey_Task_TrainingFrequency>> GetTaskTrainingFrequencyAsync()
        {
            var difTaskFrequency = await _dIFSurvey_Task_TrainingFrequencyService.AllAsync();
            return difTaskFrequency.ToList();
        }
    }
}
