using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ILA_StudentEvaluation_LinkService : Common.Service<ILA_StudentEvaluation_Link>, IILA_StudentEvaluation_LinkService
    {
        public ILA_StudentEvaluation_LinkService(IILA_StudentEvaluation_LinkRepository repository, IILA_StudentEvaluation_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILA_StudentEvaluation_Link>> GetStudentEvaluationsLinksByIlaIdEvalIdAsync(List<int> ilaIds, List<int> evalIds)
        {
            return (await FindWithIncludeAsync(r => ilaIds.Contains(r.ILAId) && evalIds.Contains(r.studentEvalFormID), new string[] { "ILA.Provider"})).ToList();
        }
    }
}
