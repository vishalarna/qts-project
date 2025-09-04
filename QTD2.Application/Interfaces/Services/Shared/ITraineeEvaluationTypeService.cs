using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EvalutionType;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITraineeEvaluationTypeService
    {
        public Task<List<TraineeEvaluationType>> GetAsync();

        public Task<TraineeEvaluationType> GetAsync(int id);

        public Task<TraineeEvaluationType> CreateAsync(TraineeEvaluationTypeCreateOptions options);

        public Task<TraineeEvaluationType> UpdateAsync(int id, TraineeEvaluationTypeUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
