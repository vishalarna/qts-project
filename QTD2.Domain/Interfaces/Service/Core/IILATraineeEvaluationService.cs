using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILATraineeEvaluationService : Common.IService<ILATraineeEvaluation>
    {
        System.Threading.Tasks.Task<List<ILATraineeEvaluation>> GetLinkedTestsAsync(int ilaId);
        System.Threading.Tasks.Task<List<ILATraineeEvaluation>> GetLinkedTestsWithILANumberAsync(int ilaId);
        System.Threading.Tasks.Task<List<ILATraineeEvaluation>> GetLinkedTestsByTestIdAsync(int testId);
        System.Threading.Tasks.Task<List<ILATraineeEvaluation>> GetLinkedTestsByTestTypeAndStatusAsync(List<int> ilaIds,List<int> testTypeIds,List<int> testStatusIds);
    }
}
