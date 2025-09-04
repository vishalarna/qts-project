using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingProgramService : Common.IService<TrainingProgram>
    {
        System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramAsync();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramWithTypesAsync();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetTrainingProgramCompactWithIncludeTypeAndILALinks();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetActiveVersionTitleWithNoReviewAsync();
        System.Threading.Tasks.Task<int> GetActiveTrainingProgramsWithReviewInDraftCountAsync();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(int positionId, int trainingProgramTypeId);
        System.Threading.Tasks.Task<List<TrainingProgram>> GetAllAsync();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetInitialTrainingProgramsAsync();
        System.Threading.Tasks.Task<List<TrainingProgram>> GetTrainingProgramsByIdAndPositionIdsAsync(List<int> positionIds, string trainingProgramId);
        public System.Threading.Tasks.Task<List<TrainingProgram>> GetAllTrainingProgramLinksWithTrainingProgramReviewAsync();
    }
}
