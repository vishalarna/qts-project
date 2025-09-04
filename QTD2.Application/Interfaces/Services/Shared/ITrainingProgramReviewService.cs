using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Infrastructure.Model.TrainingProgramReview;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingProgramReviewService
    {
        public System.Threading.Tasks.Task<TrainingProgramReview_ViewModel> CreateAsync(TrainingProgramReview_ViewModel trainingProgramReview_ViewModel);
        public Task<TrainingProgramReview_ViewModel> GetAsync(int id);
        public Task<TrainingProgramReview_ViewModel> UpdateAsync(int id,TrainingProgramReview_ViewModel trainingProgramReview_ViewModel);
        public Task<TrainingProgramReview_ViewModel> CopyAsync(int id);
        public System.Threading.Tasks.Task DeleteAsync(int id);
        public System.Threading.Tasks.Task ActivateAsync(int id);
        public System.Threading.Tasks.Task InactivateAsync(int id);
        public Task<TrainingProgramReview_OverviewViewModel> GetOverviewAsync();
    }
}
