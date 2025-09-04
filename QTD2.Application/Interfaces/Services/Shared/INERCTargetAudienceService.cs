using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.NERCTargetAudience;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface INERCTargetAudienceService
    {
        public Task<List<NERCTargetAudience>> GetAsync();

        public Task<NERCTargetAudience> GetAsync(int id);

        public Task<NERCTargetAudience> CreateAsync(NERCTargetAudienceCreateOptions options);

        public Task<NERCTargetAudience> UpdateAsync(int id, NERCTargetAudienceUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
