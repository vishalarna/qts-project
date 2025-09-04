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
    public class Version_EnablingObjective_Position_LinkService : Common.Service<Version_EnablingObjective_Position_Link>, IVersion_EnablingObjective_Position_LinkService
    {
        public Version_EnablingObjective_Position_LinkService(IVersion_EnablingObjective_Position_LinkRepository repository, IVersion_EnablingObjective_Position_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
