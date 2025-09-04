using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_PerformTraineeEvalService : Common.IService<ILA_PerformTraineeEval>
    {
        public Task<ILA_PerformTraineeEval> GetILAPerformTraineeByILAIdAsync(int ilaId);
    }
}
