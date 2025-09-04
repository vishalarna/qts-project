using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ILA_AssessmentTool_LinkRepository : Common.Repository<ILA_AssessmentTool_Link>, IILA_AssessmentTool_LinkRepository
    {
        public ILA_AssessmentTool_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
