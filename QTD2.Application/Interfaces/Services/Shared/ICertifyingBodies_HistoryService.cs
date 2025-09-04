using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CertifyingBody_History;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertifyingBodies_HistoryService
    {
        public Task<CertifyingBody_History> CreateAsync(CertifyingBody_HistoryCreateOptions options);

        public Task<CertifyingBody_History> UpdateAsync(int id, CertifyingBody_HistoryCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<CertifyingBody_History>> GetAllCertCatHistories();

        public Task<CertifyingBody_History> GetCertCatHistory(int id);
    }
}
