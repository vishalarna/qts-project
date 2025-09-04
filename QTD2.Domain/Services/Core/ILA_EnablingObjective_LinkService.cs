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
    public class ILA_EnablingObjective_LinkService : Common.Service<ILA_EnablingObjective_Link>, IILA_EnablingObjective_LinkService
    {
        public ILA_EnablingObjective_LinkService(IILA_EnablingObjective_LinkRepository repository, IILA_EnablingObjective_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<ILA_EnablingObjective_Link>> GetILA_EnablingObjective_LinksByILAId(int ilaId)
        {
            return (await FindWithIncludeAsync(r => ilaId == r.ILAId, new[] { "EnablingObjective.EnablingObjective_Topic", "EnablingObjective.EnablingObjective_Category", "EnablingObjective.EnablingObjective_SubCategory" })).ToList();
        }
    }
}
