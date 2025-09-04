using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.TrainingProgram;
using QTD2.Infrastructure.Model.TrainingProgram_ILA_Link;
using QTD2.Infrastructure.Model.TrainingProgramReview;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingProgramService
    {
        public Task<List<TrainingProgram>> GetAsync();

        public Task<List<TrainingProgram>> GetTPByPositionIdAsync(int posId, string trainingProgramType = null);

        public Task<TrainingProgram> GetAsync(int id);

        public Task<List<TrainingProgram>> GetByPositionIdAsync(int positionId);

        public Task<TrainingProgram> CreateAsync(TrainingProgramCreateOptions options);

        public Task<TrainingProgram> UpdateAsync(int id, TrainingProgramUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public Task<List<ILAWithCountOptions>> GetLinkedILAs(int id);

        public Task<TrainingProgram> LinkILA(int trainingProgramId, TrainingProgram_ILA_LinkCreateOptions options);

        public System.Threading.Tasks.Task UnlinkILA(int trainingProgramId, int[] ilaId);

        public Task<List<TrainingProgram>> GetTrainingProgramILAIsLinkedTo(int id);

        public Task<TrainingProgramVM> GetStatsCount();

        public Task<List<TrainingProgram>> GetTrainingProgamsByFilter(string filter, TrainingProgramFilterOptions options);

        public System.Threading.Tasks.Task DeactivateAsync(int id);

        public System.Threading.Tasks.Task ActivateAsync(int id);

        public Task<TrainingProgram> PublishTrainingProgramAsync(int id, TrainingProgram_HistoryCreateOption options);

        public Task<List<TrainingProgram>> GetActiveInactiveList(string name, bool status);
        
        public Task<List<TrainingProgram_VersionTitleViewModel>> GetActiveVersionTitleWithNoReviewAsync();
        
        public Task<List<TrainingProgram_VersionTitleViewModel>> GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(int positionId, int trainingProgramTypeId);
      
        public Task<List<TrainingProgram>> GetTrainingProgramLinksAndTrainingProgramReviewAsync();
    }
}
