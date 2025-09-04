using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_Reference;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITask_ReferenceService
    {
        public Task<List<Task_Reference>> GetAllAsync();

        public Task<Task_Reference> GetAsync(int id);

        public Task<Task_Reference> UpdateAsync(int id, Task_ReferenceCreateOptions options);

        public Task<Task_Reference> CreateAsync(Task_ReferenceCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

    }
}
