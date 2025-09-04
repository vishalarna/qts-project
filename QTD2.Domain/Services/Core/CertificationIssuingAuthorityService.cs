using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class CertificationIssuingAuthorityService : Common.Service<Entities.Core.CertificationIssuingAuthority>, Interfaces.Service.Core.ICertificationIssuingAuthorityService
    {
        public CertificationIssuingAuthorityService(ICertificationIssuingAuthorityRepository cert_IssuingAuthorityRepository, ICertificationIssuingAuthorityValidation cert_IssuingAuthorityValidation)
            : base(cert_IssuingAuthorityRepository, cert_IssuingAuthorityValidation)
        {
        }
    }
}
