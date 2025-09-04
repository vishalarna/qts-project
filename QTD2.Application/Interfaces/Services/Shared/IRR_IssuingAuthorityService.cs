using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.RR_IssuingAuthority;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IRR_IssuingAuthorityService
    {
        public Task<List<RR_IssuingAuthority>> GetAsync();

        public Task<List<RR_IssuingAuthority>> GetComapnctRR_IAWithoutIncludesAsync();

        public Task<RR_IssuingAuthority> GetAsync(int id);

        public Task<RR_IssuingAuthority> CreateAsync(RR_IssuingAuthorityCreateOptions options);

        public Task<RR_IssuingAuthority> UpdateAsync(int id, RR_IssuingAuthorityCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<RR_IssuingAuthorityCompact>> getWithRR();
    }
}
