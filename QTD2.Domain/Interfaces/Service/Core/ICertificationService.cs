using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ICertificationService : IService<Certification>
    {
        Task<List<Certification>> GetByListOfIDsWithSubRequirementsAsync(List<int> list);
        Task<List<Certification>> GetCertificationListAsync();
        Task<List<Certification>> GetCertificationsByIdAsync(List<int> certificationIds);
        Task<List<Certification>> GetWithCertifyingBodyAndReqsByIdAsync(List<int> certificationIds);
        Task<List<Certification>> GetNercCertificatesAsync();
    }
}
