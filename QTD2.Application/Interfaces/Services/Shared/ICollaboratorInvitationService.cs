using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CollaboratorInvitation;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICollaboratorInvitationService
    {
        public Task<List<CollaboratorInvitation>> GetAsync();

        public Task<CollaboratorInvitation> GetAsync(int id);

        public Task<CollaboratorInvitation> CreateAsync(CollaboratorInvitationCreateOptions options);

        public Task<CollaboratorInvitation> UpdateAsync(int id, CollaboratorInvitationUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
