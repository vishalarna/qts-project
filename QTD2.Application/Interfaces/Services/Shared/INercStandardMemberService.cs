using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.NercStandardMember;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface INercStandardMemberService
    {
        public Task<List<NercStandardMember>> GetAsync(int stdId);

        public Task<NercStandardMember> GetAsync(int stdId, int id);

        public Task<NercStandardMember> CreateAsync(int stdId, NercStandardMemberCreateOptions options);

        public Task<NercStandardMember> UpdateAsync(int stdId, int id, NercStandardMemberUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int stdId, int id);

        public System.Threading.Tasks.Task ActiveAsync(int stdId, int id);

        public System.Threading.Tasks.Task InActiveAsync(int stdId, int id);
    }
}
