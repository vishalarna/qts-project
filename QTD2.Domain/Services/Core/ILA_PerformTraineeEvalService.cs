using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ILA_PerformTraineeEvalService : Common.Service<ILA_PerformTraineeEval>, IILA_PerformTraineeEvalService
    {
        public ILA_PerformTraineeEvalService(IILA_PerformTraineeEvalRepository repository, IILA_PerformTraineeEvalValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ILA_PerformTraineeEval> GetILAPerformTraineeByILAIdAsync(int ilaId)
        {
            return (await FindAsync(x => x.ILAId == ilaId)).FirstOrDefault();
        }
    }
}
