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
    public class ILA_NERCAudience_LinkService : Common.Service<ILA_NERCAudience_Link>, IILA_NERCAudience_LinkService
    {
        public ILA_NERCAudience_LinkService(IILA_NERCAudience_LinkRepository repository, IILA_NERCAudience_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILA_NERCAudience_Link>> GetActiveNercAudienceLinks(int ilaId)
        {
            var nercAudence = await FindWithIncludeAsync(x => x.ILAId == ilaId && x.NERCTargetAudience.Active == true && x.Active == true, new string[] { "NERCTargetAudience" });
            return nercAudence.ToList();
        }
    }
}
