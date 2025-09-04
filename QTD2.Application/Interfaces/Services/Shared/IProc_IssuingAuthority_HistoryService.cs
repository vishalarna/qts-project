using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.IssuingAuthorityStatusHistory;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProc_IssuingAuthority_HistoryService
    {
        public Task<List<Proc_IssuingAuthority_History>> GetAsync();

        public Task<Proc_IssuingAuthority_History> GetAsync(int id);

        public Task<Proc_IssuingAuthority_History> CreateAsync(Proc_IssuingAuthority_HistoryCreateOptions options);

        public Task<Proc_IssuingAuthority_History> UpdateAsync(int id, Proc_IssuingAuthority_HistoryCreateOptions options);

        public Task<Proc_IssuingAuthority_History> DeleteAsync(int id);

        public Task<Proc_IssuingAuthority_History> ActiveAsync(int id);

        public Task<Proc_IssuingAuthority_History> DeactivateAsync(int id);
    }
}
