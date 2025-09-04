using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingProgramReviewService : Common.IService<TrainingProgramReview>
    {
        public System.Threading.Tasks.Task<TrainingProgramReview> GetAsync(int id);
        public System.Threading.Tasks.Task<List<TrainingProgramReview>> GetAllAsync();
        public System.Threading.Tasks.Task<int> GetActiveInitialTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<int> GetInactiveInitialTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<int> GetActiveContinuingTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<int> GetInactiveContinuingTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<int> GetActiveCycleTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<int> GetInactiveCycleTrainingProgramReviewsAsync();
        public System.Threading.Tasks.Task<List<TrainingProgramReview>> GetReviewsForDocumentStorageAsync();
        public System.Threading.Tasks.Task<List<TrainingProgramReview>> GetReviewsByTrainingProgramsAsync(List<int> trainingProgramIDs ,DateTime startDate,DateTime endDate);
    }
}
