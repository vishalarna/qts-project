using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IILA_StudentEvaluation_LinkService : Common.IService<ILA_StudentEvaluation_Link>
    {
        public System.Threading.Tasks.Task<List<ILA_StudentEvaluation_Link>> GetStudentEvaluationsLinksByIlaIdEvalIdAsync(List<int> ilaIds, List<int> evalIds);
    }
}
