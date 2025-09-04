using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.TrainingProgramType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITrainingProgramTypeService
    {
        public Task<List<TrainingProgramType>> GetAsync();

        public Task<TrainingProgramType> GetAsync(int id);

        public Task<TrainingProgramType> CreateAsync(TrainingProgramTypeCreateOptions options);

        public Task<TrainingProgramType> UpdateAsync(int id, TrainingProgramTypeCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
