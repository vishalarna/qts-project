using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertificationHistoryService
    {
        public Task<Certification_History> CreateAsync(Certification_HistoryCreateOptions options);

        public Task<Certification_History> UpdateAsync(int id, Certification_HistoryCreateOptions options);
        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Certification_History>> GetAllCertCatHistories();
        public Task<Certification_History> GetCertCatHistory(int id);

        public Task<List<CertificationLatestActivityVM>> GetHistoryAsync(bool getLatest);

    }
}
