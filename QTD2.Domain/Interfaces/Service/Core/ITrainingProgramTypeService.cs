using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITrainingProgramTypeService : Common.IService<TrainingProgramType>
    {
        System.Threading.Tasks.Task<List<TrainingProgramType>> GetTPTypeWithProgramsAndReviews();
        System.Threading.Tasks.Task<List<TrainingProgramType>> GetAllTrainingProgramTypesAsync();
        System.Threading.Tasks.Task<List<TrainingProgramType>> GetTrainingProgramTypesAsync();

    }
}
