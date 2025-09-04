using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class TrainingProgramTypeService : Common.Service<TrainingProgramType>, ITrainingProgramTypeService
    {
        public TrainingProgramTypeService(ITrainingProgramTypeRepository trainingProgramtypeRepository, ITrainingProgramTypeValidation trainingProgramtypeValidation)
            : base(trainingProgramtypeRepository, trainingProgramtypeValidation)
        {
        }

        public async Task<List<TrainingProgramType>> GetTPTypeWithProgramsAndReviews()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "TrainingPrograms.TrainingProgramReviews" });
            return queryable.ToList();
        }
        public async System.Threading.Tasks.Task<List<TrainingProgramType>> GetAllTrainingProgramTypesAsync()
        {
            var trainingProgramTypes = (await FindAsync(x => x.Active)).ToList(); ;
            return trainingProgramTypes;
        }
        public async System.Threading.Tasks.Task<List<TrainingProgramType>> GetTrainingProgramTypesAsync()
        {
            var trainingProgramTypes = (await FindWithIncludeAsync(x => x.Active, new[] { "TrainingPrograms" })).ToList() ;
            return trainingProgramTypes;

        }

    }
}
