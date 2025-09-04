using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILA_StudentEvaluation_LinkRepository : Common.Repository<ILA_StudentEvaluation_Link>, IILA_StudentEvaluation_LinkRepository
    {
        public ILA_StudentEvaluation_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
