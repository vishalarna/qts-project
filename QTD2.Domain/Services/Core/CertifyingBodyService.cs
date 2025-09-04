using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class CertifyingBodyService : Common.Service<CertifyingBody>, ICertifyingBodyService
    {
        public CertifyingBodyService(ICertifyingBodyRepository certifyingBodyRepository, ICertifyingBodyValidation certifyingBodyValidation)
            : base(certifyingBodyRepository, certifyingBodyValidation)
        {
        }

        public async  Task<List<CertifyingBody>> GetCertifyingBodiesAsync(bool isLevelEditing)
        {
            List<Expression<Func<CertifyingBody, bool>>> predicates = new List<Expression<Func<CertifyingBody, bool>>>();
            predicates.Add(r => r.EnableCertifyingBodyLevelCEHEditing == isLevelEditing);
            return (await FindWithIncludeAsync(predicates, new string[] { "Certifications.CertificationSubRequirements", "Certifications.ILACertificationLinks"  })).ToList();
        }
    }
}
