using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace QTD2.Domain.Services.Core
{
    public class CertificationService : Common.Service<Certification>, ICertificationService
    {
        public CertificationService(ICertificationRepository certificationRepository, ICertificationValidation certificationValidation)
            : base(certificationRepository, certificationValidation)
        {
        }

        public async Task<List<Certification>> GetByListOfIDsWithSubRequirementsAsync(List<int> list)
        {
            return (await FindWithIncludeAsync(r => list.Contains(r.Id), new string[] { "CertificationSubRequirements" })).ToList();
        }

        public async Task<List<Certification>> GetCertificationListAsync()
        {
            return (await AllWithIncludeAsync(new string[] { "CertificationSubRequirements", "CertifyingBody" })).ToList();
        } 
        
        public async Task<List<Certification>> GetCertificationsByIdAsync(List<int> certificationIds)
        {
            List<Expression<Func<Certification, bool>>> predicates = new List<Expression<Func<Certification, bool>>>();

            predicates.Add(r => certificationIds.Contains(r.Id));

            return (await FindWithIncludeAsync(predicates, new string[] { "EmployeeCertifications" })).ToList();
        }

        public async Task<List<Certification>> GetWithCertifyingBodyAndReqsByIdAsync(List<int> certificationIds)
        {
            List<Expression<Func<Certification, bool>>> predicates = new List<Expression<Func<Certification, bool>>>();

            predicates.Add(r => certificationIds.Contains(r.Id));

            return (await FindWithIncludeAsync(predicates, new string[] { "CertificationSubRequirements", "CertifyingBody" })).ToList();
        }

        public async Task<List<Certification>> GetNercCertificatesAsync()
        {
            List<Expression<Func<Certification, bool>>> predicates = new List<Expression<Func<Certification, bool>>>();
            predicates.Add(r => r.CertifyingBody.Name.ToUpper() == "NERC");
            return (await FindAsync(predicates)).ToList();
        }

        public async Task<List<Certification>> GetCertificationSubRequirementByCertificationIdAsync(int certificationId)
        {
            List<Expression<Func<Certification, bool>>> predicates = new List<Expression<Func<Certification, bool>>>();
            predicates.Add(r => r.Id == certificationId);
            return (await FindWithIncludeAsync(predicates, new string[] { "CertificationSubRequirements"})).ToList();
        }
    }
}
