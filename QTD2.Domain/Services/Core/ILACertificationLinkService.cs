using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ILACertificationLinkService : Common.Service<ILACertificationLink>, IILACertificationLinkService
    {
        public ILACertificationLinkService(IILACertificationLinkRepository repository, IILACertificationLinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ILACertificationLink>> GetByCertificationIdAsync(int CertificationId)
        {
            var iLACertificationLinks = await FindAsync(r => r.CertificationId == CertificationId);
            return iLACertificationLinks.ToList();
        }
    }
}
