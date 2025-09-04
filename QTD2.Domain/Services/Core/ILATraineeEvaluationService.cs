using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ILATraineeEvaluationService : Common.Service<ILATraineeEvaluation>, IILATraineeEvaluationService
    {
        public ILATraineeEvaluationService(IILATraineeEvaluationRepository repository, IILATraineeEvaluationValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILATraineeEvaluation>> GetLinkedTestsAsync(int ilaId)
        {
            var ilaTraineeEvals = await FindWithIncludeAsync(r => r.ILAId == ilaId, new string[] { "TestType", "Test" });
            return ilaTraineeEvals.ToList();
        }

        public async Task<List<ILATraineeEvaluation>> GetLinkedTestsWithILANumberAsync(int ilaId)
        {
            var ilaTraineeEvals = await FindWithIncludeAsync(r => r.ILAId == ilaId, new string[] { "Test", "ILA", "TestType" });
            return ilaTraineeEvals.ToList();
        } 
        
        public async Task<List<ILATraineeEvaluation>> GetLinkedTestsByTestIdAsync(int testId)
        {
            var ilaTraineeEvals = await FindAsync(r => r.TestId == testId);
            return ilaTraineeEvals.ToList();
        }

        public async System.Threading.Tasks.Task<List<ILATraineeEvaluation>> GetLinkedTestsByTestTypeAndStatusAsync(List<int> ilaIds, List<int> testTypeIds, List<int> testStatusIds)
        {
            List<Expression<Func<ILATraineeEvaluation, bool>>> predicates = new List<Expression<Func<ILATraineeEvaluation, bool>>>();
            if (ilaIds.Count() > 0)
            {
                predicates.Add(x => ilaIds.Contains(x.ILAId));
            }
            predicates.Add(x => testTypeIds.Contains(x.TestTypeId.Value));
            predicates.Add(x => testStatusIds.Contains(x.Test.TestStatusId));
            return (await FindWithIncludeAsync(predicates, new[] { "ILA", "Test.TestStatus", "Test.Test_Item_Links" }, true)).ToList();
        }
    }
}
