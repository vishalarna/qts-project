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
    public class Task_Reference_LinkService : Common.Service<Task_Reference_Link>, ITask_Reference_LinkService
    {
        public Task_Reference_LinkService(ITask_Reference_LinkRepository repository, ITask_Reference_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
