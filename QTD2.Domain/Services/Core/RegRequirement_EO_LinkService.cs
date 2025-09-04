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
    public class RegRequirement_EO_LinkService : Common.Service<RegRequirement_EO_Link>, IRegRequirement_EO_LinkService
    {
        public RegRequirement_EO_LinkService(IRegRequirement_EO_LinkRepository repository, IRegRequirement_EO_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
