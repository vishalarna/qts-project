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
    public class ILACustomObjective_LinkService : Common.Service<ILACustomObjective_Link>, IILACustomObjective_LinkService
    {
        public ILACustomObjective_LinkService(IILACustomObjective_LinkRepository repository, IILACustomObjective_LinkValidation validation)
            : base(repository, validation)
        {
        }
        public async System.Threading.Tasks.Task<List<ILACustomObjective_Link>> GetILA_CustomObjective_LinksByILAId(int ilaId)
        {
            return (await FindWithIncludeAsync(r => ilaId == r.ILAId, new[] { "CustomEnablingObjective.EnablingObjective_Topic", "CustomEnablingObjective.EnablingObjective_Category", "CustomEnablingObjective.EnablingObjective_SubCategory" })).ToList();
        }
    }
}
