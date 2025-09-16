using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertifyingBodiesService
    {
        public Task<List<CertifyingBody>> GetAsync();

        public Task<CertifyingBody> GetAsync(int id);

        public Task<CertifyingBody> GetWithoutIncludeAsync(int id);

        public Task<CertifyingBody> CreateAsync(CertifyingBodyCreateOptions options);

        public Task<CertifyingBody> UpdateAsync(int id, CertifyingBodyCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

        public Task<List<Certification>> GetCertificationsAsync(int id);

        public Task<Certification> GetCertificationAsync(int certificationId);

        public Task<Certification> GetCertificationAsync(int certifyingBodyId, string certificationName);

        public Task<Certification> CreateCertificationAsync(int certifyingBodyId, CertificationCreateOptions options);

        public Task<Certification> UpdateCertificationAsync(int certificationId, CertificateUpdateOptions options);

        public System.Threading.Tasks.Task DeleteCertificationAsync(int certificationId);

        public Task<List<CertifyingBodyCompactOptions>> GetCertCategoryWithCert();

        public Task<bool> IsEmployeeCertification(int id);

        public Task<List<CertifyingBodyWithSubRequirementsVM>> GetCertifyingBodiesByLevelEditingAsync(bool isLevelEditing, int ilaId);
        public Task<List<SubRequirementVM>> GetCertifyingBodiesWithSubRequirementsAsync(bool isLevelEditing);
    }
}
