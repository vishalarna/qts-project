using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Task_CollaboratorInvitation;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ITask_CollaboratorInvitationService
    {
        public Task<List<Task_CollaboratorInvitation>> GetAllAsync();

        public Task<List<Task_CollaboratorInvitation>> GetCollaboratorsForTask(int taskId);

        public Task<Task_CollaboratorInvitation> GetAsync(int id);

        public Task<Task_CollaboratorInvitation> UpdateAsync(int id, Task_CollaboratorInvitationCreateOptions options);

        public Task<Task_CollaboratorInvitation> CreateAsync(Task_CollaboratorInvitationCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
