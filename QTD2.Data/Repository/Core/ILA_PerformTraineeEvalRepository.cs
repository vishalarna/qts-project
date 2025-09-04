using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILA_PerformTraineeEvalRepository : Common.Repository<ILA_PerformTraineeEval>, IILA_PerformTraineeEvalRepository
    {
        public ILA_PerformTraineeEvalRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
