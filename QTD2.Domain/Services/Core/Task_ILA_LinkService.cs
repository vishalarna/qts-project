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
    public class Task_ILA_LinkService : Common.Service<Task_ILA_Link>, ITask_ILA_LinkService
    {
        public Task_ILA_LinkService(ITask_ILA_LinkRepository repository, ITask_ILA_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
