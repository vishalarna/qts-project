using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EvalutionType;
using QTD2.Infrastructure.Model.ILATraineeEvaluation;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IILATraineeEvaluationService
    {
        public Task<List<ILATraineeEvaluation>> GetAsync();

        public Task<ILATraineeEvaluation> GetAsync(int id);

        public Task<ILATraineeEvaluation> CreateAsync(ILATraineeEvaluationCreateOptions options);

        public Task<ILATraineeEvaluation> UpdateAsync(int id, ILATraineeEvaluationCreateOptions options);

        public Task<ILATraineeEvaluation> CopyTraineeEvaluationAsync(int id);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<IlaTraineeEvaluationListVm>> GetTraineeEvaluationByILAAsync(int id);
        public Task<bool> ChangeTraineeEvaluationStatus(TraineeEvaluationStatusVM data);

        public Task<bool> RemoveTraineeTypeIlaIdAsync(int ilaId);

    }
}
