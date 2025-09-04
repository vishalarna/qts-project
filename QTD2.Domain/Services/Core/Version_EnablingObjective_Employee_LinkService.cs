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
    public class Version_EnablingObjective_Employee_LinkService : Common.Service<Version_EnablingObjective_Employee_Link>, IVersion_EnablingObjective_Employee_LinkService
    {
        public Version_EnablingObjective_Employee_LinkService(IVersion_EnablingObjective_Employee_LinkRepository repository, IVersion_EnablingObjective_Employee_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
