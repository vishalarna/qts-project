using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.Certification_History;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertificationService
    {
        public Task<List<Certification>> GetAsync();
        public Task<bool> SaveIlaCertificationLinkAsync(ILACertificationVM data);


        public Task<Certification> GetAsync(int Certid);
        public Task<ILACertificationVM> GetWithIlaDataAsync(int Certid,int ilaId);
        public Task<bool> RemoveLinkWithIlaDataAsync(int Certid,int ilaId);

        public Task<Certification> CreateAsync(CertificationCreateOptions options);

        public Task<Certification> UpdateAsync(int Certid, CertificationCreateOptions options);


        public System.Threading.Tasks.Task DeleteAsync(Certification_HistoryCreateOptions options);
        public System.Threading.Tasks.Task ActiveAsync(Certification_HistoryCreateOptions options);

        public Task<int> getCount();

        public Task<CertificationStatsVM> GetStatsCount();
        public Task<List<string>> getLinkedCertifications(int ilaId);

        public System.Threading.Tasks.Task InActiveAsync(Certification_HistoryCreateOptions options);

        public Task<List<Certification>> GetCertActiveInactive(string option);

        public Task<List<CertifyingBody>> GetCatActiveInactive(string option);
        public Task<List<Certification>> GetNercCertificatesAsync();
    }
}
