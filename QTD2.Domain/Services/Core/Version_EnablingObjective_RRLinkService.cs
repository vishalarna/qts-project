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
    public class Version_EnablingObjective_RRLinkService : Common.Service<Version_EnablingObjective_RRLink>, IVersion_EnablingObjective_RRLinkService
    {
        public Version_EnablingObjective_RRLinkService(IVersion_EnablingObjective_RRLinkRepository repository, IVersion_EnablingObjective_RRLinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
