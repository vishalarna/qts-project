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
    public class ILA_AssessmentTool_LinkService : Common.Service<ILA_AssessmentTool_Link>, IILA_AssessmentTool_LinkService
    {
        public ILA_AssessmentTool_LinkService(IILA_AssessmentTool_LinkRepository repository, IILA_AssessmentTool_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILA_AssessmentTool_Link>> GetActiveAssesmentToolLinks(int ilaId)
        {
            var tools = await FindWithIncludeAsync(x => x.ILAId == ilaId && x.AssessmentTool.Active == true && x.Active == true,new string[] { "AssessmentTool" });
            return tools.ToList();
        }
    }
}
