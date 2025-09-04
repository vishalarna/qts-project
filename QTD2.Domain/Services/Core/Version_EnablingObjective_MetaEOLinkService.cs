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
    public class Version_EnablingObjective_MetaEOLinkService : Common.Service<Version_EnablingObjective_MetaEOLink>, IVersion_EnablingObjective_MetaEOLinkService
    {
        public Version_EnablingObjective_MetaEOLinkService(IVersion_EnablingObjective_MetaEOLinkRepository repository, IVersion_EnablingObjective_MetaEOLinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
