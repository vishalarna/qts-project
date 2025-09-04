using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.NercStandard;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface INercStandardService
    {
        public Task<List<NercStandard>> GetAsync();

        public Task<NercStandard> GetAsync(int id);

        public Task<NercStandard> CreateAsync(NercStandardCreateOptions options);

        public Task<NercStandard> UpdateAsync(int id, NercStandardUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
