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
    public class Task_MetaTask_LinkService : Common.Service<Task_MetaTask_Link>, ITask_MetaTask_LinkService
    {
        public Task_MetaTask_LinkService(ITask_MetaTask_LinkRepository repository, ITask_MetaTask_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
