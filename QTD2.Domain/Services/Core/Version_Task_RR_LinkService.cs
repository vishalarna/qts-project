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
    public class Version_Task_RR_LinkService : Common.Service<Version_Task_RR_Link>, IVersion_Task_RR_LinkService
    {
        public Version_Task_RR_LinkService(IVersion_Task_RR_LinkRepository repository, IVersion_Task_RR_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
