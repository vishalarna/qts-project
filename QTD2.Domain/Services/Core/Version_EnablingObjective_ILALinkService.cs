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
    public class Version_EnablingObjective_ILALinkService : Common.Service<Version_EnablingObjective_ILALink>, IVersion_EnablingObjective_ILALinkService
    {
        public Version_EnablingObjective_ILALinkService(IVersion_EnablingObjective_ILALinkRepository repository, IVersion_EnablingObjective_ILALinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
