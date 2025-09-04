using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.CertificationIssuingAuthority;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ICertificationIssuingAuthorityService
    {
        public Task<List<CertificationIssuingAuthority>> GetAsync();

        public Task<CertificationIssuingAuthority> GetAsync(int id);

        public Task<CertificationIssuingAuthority> CreateAsync(CertificationIssuingAuthorityCreateOptions options);

        public Task<CertificationIssuingAuthority> UpdateAsync(int id, CertificationIssuingAuthorityCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);

    }
}
