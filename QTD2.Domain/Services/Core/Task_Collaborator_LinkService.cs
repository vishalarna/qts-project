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

namespace QTD2.Domain.Services.Core
{
    public class Task_Collaborator_LinkService : Common.Service<Task_Collaborator_Link>, ITask_Collaborator_LinkService
    {
        public Task_Collaborator_LinkService(ITask_Collaborator_LinkRepository repository, Interfaces.Validation.Core.ITask_Collaborator_LinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
