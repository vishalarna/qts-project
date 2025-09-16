using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.CertifyingBody_History;
using IEmployeeCertificationDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeCertificationService;
using QTD2.Domain.Certifications.Models;

namespace QTD2.Application.Services.Shared
{
    public class CertiyfingBodiesService : ICertifyingBodiesService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<CertiyfingBodiesService> _localizer;
        private readonly Domain.Interfaces.Service.Core.ICertifyingBodyService _certifyingBodyService;
        private readonly Domain.Interfaces.Service.Core.ICertificationService _certificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly CertifyingBody _certifyingBody;
        private readonly IEmployeeCertificationDomainService _empCertificationService;
        private readonly Domain.Interfaces.Service.Core.IILAService _iLAService;

        public CertiyfingBodiesService(
            Domain.Interfaces.Service.Core.ICertifyingBodyService certifyingBodyService,
            Domain.Interfaces.Service.Core.ICertificationService certificationService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<CertiyfingBodiesService> localizer,
            UserManager<AppUser> userManager,
            IEmployeeCertificationDomainService empCertificationService,
            Domain.Interfaces.Service.Core.IILAService iLAService)
        {
            _certifyingBodyService = certifyingBodyService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _certificationService = certificationService;
            _localizer = localizer;
            _userManager = userManager;
            _certifyingBody = new CertifyingBody();
            _empCertificationService = empCertificationService;
            _iLAService = iLAService;
        }

        public async Task<CertifyingBody> CreateAsync(CertifyingBodyCreateOptions options)
        {
            var certBody = new CertifyingBody(options.Name, options.Desc, options.Website, options.EffectiveDate, options.IsNERC);

            var obj = (await _certifyingBodyService.FindAsync(x => x.Name == options.Name)).FirstOrDefault();
            if (obj == null)
            {
                obj = new CertifyingBody(options.Name, options.Desc, options.Website, options.EffectiveDate, options.IsNERC);
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }

            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certBody, CertifyingBodyOperations.Create);
            if (result.Succeeded)
            {
                var validationResult = await _certifyingBodyService.AddAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }

                return obj;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<List<CertifyingBody>> GetAsync()
        {
            var certBodies = await _certifyingBodyService.AllQueryWithInclude(new string[] { nameof(_certifyingBody.Certifications) }).ToListAsync(); ;
            certBodies = certBodies.Where(certBody => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certBody, CertifyingBodyOperations.Read).Result.Succeeded).ToList();
            return certBodies?.ToList();
        }

        public async Task<CertifyingBody> GetAsync(int id)
        {
            var certifyingBdy = await _certifyingBodyService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (certifyingBdy != null)
            {
                certifyingBdy.Certifications = await _certificationService.FindQuery(x => x.CertifyingBodyId == certifyingBdy.Id).ToListAsync();
                for (int i = 0; i < certifyingBdy.Certifications.Count; i++)
                {
                    certifyingBdy.Certifications.ToList()[i].EmployeeCertifications = await _empCertificationService.FindQuery(x => x.CertificationId == certifyingBdy.Certifications.ToList()[i].Id).ToListAsync();
                }
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certifyingBdy, CertifyingBodyOperations.Read);
                if (result.Succeeded)
                {
                    return certifyingBdy;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }

            return certifyingBdy;
        }

        public async Task<CertifyingBody> GetWithoutIncludeAsync(int id)
        {
            var certifyingBdy = await _certifyingBodyService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if (certifyingBdy != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certifyingBdy, CertifyingBodyOperations.Read);
                if (result.Succeeded)
                {
                    return certifyingBdy;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["CertifyingBodyNotFound"]);
            }
        }

        public async Task<bool> IsEmployeeCertification(int id)
        {
            var certifyingBdy = await _certifyingBodyService.FindWithIncludeAsync(x => x.Id == id, new string[] { "Certifications", "Certifications.EmployeeCertifications" });
            foreach (var cb in certifyingBdy)
            {
                foreach (var cert in cb.Certifications)
                {
                    if (cert.EmployeeCertifications.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            //foreach (var cb in certifyingBdy)
            //{
            //    if (cb.IsPublished == true)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }

        public async Task<List<Certification>> GetCertificationsAsync(int certifyingBodyId)
        {
            var certifyingBody = await GetAsync(certifyingBodyId);
            var certifications = (await _certificationService.FindAsync(x => x.CertifyingBodyId == certifyingBody.Id)).ToList();
            certifications = certifications.Where(cert => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, cert, CertificationOperations.Read).Result.Succeeded).ToList();
            return certifications;
        }

        public async Task<Certification> GetCertificationAsync(int certificationId)
        {
            var certification = await _certificationService.GetAsync(certificationId);
            if (certification != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certification, CertificationOperations.Read);
                if (result.Succeeded)
                {
                    return certification;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["CertificationNotFound"]);
            }
        }

        public async Task<Certification> CreateCertificationAsync(int certifyingBodyId, CertificationCreateOptions options)
        {
            var certifyingBody = await GetAsync(certifyingBodyId);
            var certification = await GetCertificationAsync(certifyingBodyId, options.Name);
            if (certification != null)
            {
                throw new BadHttpRequestException(message: _localizer["CertAlreadyExists"]);
            }

            certification = new Certification(options.CertifyingBodyId, options.CertAcronym, options.Name, options.CertDesc, options.RenewalTimeFrame, options.RenewalInterval,
                                        options.CreditHrsReq, options.CreditHrs, options.CertSubReq, " ", 0, options.CertMemberNum,
                                        options.CertifiedDate, options.RenewalDate, options.ExpirationDate, options.AllowRolloverHours, options.AdditionalHours, options.EffectiveDate);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certification, CertificationOperations.Create);
            if (result.Succeeded)
            {
                certification.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                certification.CreatedDate = DateTime.Now;
                var validationResult = await _certificationService.AddAsync(certification);
                if (validationResult.IsValid)
                {
                    return certification;
                }
                else
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<int> getCount()
        {
            var result = await _certifyingBodyService.AllQueryWitDeletedCount();
            return result;
        }


        public async Task<CertifyingBody> UpdateAsync(int id, CertifyingBodyCreateOptions options)
        {
            var obj = await GetAsync(id);

            obj.Name = options.Name;
            obj.Desc = options.Desc;
            obj.Website = options.Website;
            obj.EffectiveDate = options.EffectiveDate;
            obj.IsNERC = options.IsNERC;

            obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.ModifiedDate = DateTime.Now;

            var validationResult = await _certifyingBodyService.UpdateAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }


        }


        public async Task<List<CertifyingBodyCompactOptions>> GetCertCategoryWithCert()
        {
            var shCategory = await _certifyingBodyService.AllQuery().Select(x => new CertifyingBody
            {
                Id = x.Id,
                Active = x.Active,
                Notes = x.Notes,
                IsNERC = x.IsNERC,
                EffectiveDate = x.EffectiveDate,
                Website = x.Website,
                Desc = x.Desc,
                Name = x.Name,
                EnableCertifyingBodyLevelCEHEditing = x.EnableCertifyingBodyLevelCEHEditing,
            }).ToListAsync();
            var sh = await _certificationService.AllQuery().Select(x => new Certification
            {
                Id = x.Id,
                CertAcronym = x.CertAcronym,
                CertifyingBodyId = x.CertifyingBodyId,
                Name = x.Name,
                Active = x.Active
            }).ToListAsync();
            var data = shCategory.OrderBy(x => x.Name).GroupJoin(sh, x => x.Id, x => x.CertifyingBodyId, (shCategory, sh) => new
            {
                shCategory,
                Category = sh
                .Select(x => new CertificationCompactOptions(x.Id, x.CertAcronym, x.CertifyingBodyId, x.Name, x.Active, false)).OrderBy(o => o.CertAcronym).ToList()
            }).ToList();
            List<CertifyingBodyCompactOptions> shCatCompact = new List<CertifyingBodyCompactOptions>();
            foreach (var item in data)
            {
                var shCompact = new CertifyingBodyCompactOptions();
                shCompact.CertifyingBody = item.shCategory;
                shCompact.CertificationCompactOptions.AddRange(item.Category);
                shCatCompact.Add(shCompact);
            }

            return shCatCompact;
        }


        public async Task<Certification> GetCertificationAsync(int certifyingBodyId, string certificationName)
        {
            var certification = (await _certificationService.FindAsync(r => r.CertifyingBodyId == certifyingBodyId && r.Name == certificationName)).FirstOrDefault();
            if (certification != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certification, CertificationOperations.Read);
                if (result.Succeeded)
                {
                    return certification;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["CertificationNotFound"]);
            }
        }

        public async Task<Certification> UpdateCertificationAsync(int certificationId, CertificateUpdateOptions options)
        {
            var certification = await GetCertificationAsync(certificationId);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certification, CertificationOperations.Update);
            if (result.Succeeded)
            {
                // Todo Update Logic
                certification.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                certification.ModifiedDate = DateTime.Now;
                var validationResult = await _certificationService.UpdateAsync(certification);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }

                return certification;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteCertificationAsync(int certificationId)
        {
            var certification = await GetCertificationAsync(certificationId);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certification, CertificationOperations.Update);
            if (result.Succeeded)
            {
                var validationResult = await _certificationService.DeleteAsync(certification);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<CertifyingBody> UpdateAsync(int id, CertifyingBodyUpdateOptions options)
        {
            var certifyingBody = await GetAsync(id);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certifyingBody, CertifyingBodyOperations.Update);
            if (result.Succeeded)
            {
                // Todo Update Logic
                certifyingBody.Name = options.Name;
                var validationResult = await _certifyingBodyService.UpdateAsync(certifyingBody);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }

                return certifyingBody;
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var certifyingBody = await GetAsync(id);


            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, certifyingBody, CertifyingBodyOperations.Delete);
            if (result.Succeeded)
            {
                // Todo Delete Logic
                certifyingBody.Delete();

                var validationResult = await _certifyingBodyService.UpdateAsync(certifyingBody);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            //var obj = await GetAsync(id);

            //obj.Activate();

            //var validationResult = await _certifyingBodyService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}
            var proc_issu = await _certifyingBodyService.GetWithIncludeAsync(id, new string[] { nameof(_certifyingBody.Certifications) });
            List<Domain.Entities.Core.Certification> procList = new List<Domain.Entities.Core.Certification>();
            procList.AddRange(proc_issu.Certifications);
            if (proc_issu != null)
            {
                proc_issu.Activate();
                await _certifyingBodyService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new CertificationOptions();
                    options.CertId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Active Due to Issuing Authority Active";
                    await CertficationActivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["CertificationsNotFound"]);
            }


        }

        public async System.Threading.Tasks.Task CertficationActivateAsync(int id, CertificationOptions options)
        {
            var procedure = await _certificationService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, RegulatoryRequirementOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Activate();

                var validationResult = await _certificationService.UpdateAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }




        public async System.Threading.Tasks.Task CertficationDeactivateAsync(int id, CertificationOptions options)
        {
            var procedure = await _certificationService.GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, procedure, RegulatoryRequirementOperations.Delete);
            if (result.Succeeded)
            {
                procedure.Deactivate();

                var validationResult = await _certificationService.UpdateAsync(procedure);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            //var obj = await GetAsync(id);
            //obj.Deactivate();

            //var validationResult = await _certifyingBodyService.UpdateAsync(obj);
            //if (!validationResult.IsValid)
            //{
            //    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            //}
            var proc_issu = await _certifyingBodyService.GetWithIncludeAsync(id, new string[] { nameof(_certifyingBody.Certifications) });
            List<Domain.Entities.Core.Certification> procList = new List<Domain.Entities.Core.Certification>();
            procList.AddRange(proc_issu.Certifications);
            if (proc_issu != null)
            {
                proc_issu.Deactivate();
                await _certifyingBodyService.UpdateAsync(proc_issu);

                foreach (var proc in procList)
                {
                    var options = new CertificationOptions();
                    options.CertId = proc.Id;
                    options.EffectiveDate = proc_issu.EffectiveDate;
                    options.ChangeNotes = "Inactive Due to Issuing Authority Inactive";
                    await CertficationDeactivateAsync(proc.Id, options);

                }
            }
            else
            {
                throw new QTDServerException(_localizer["CertificationsNotFound"]);
            }
        }

        public async Task<List<CertifyingBodyWithSubRequirementsVM>> GetCertifyingBodiesByLevelEditingAsync(bool isLevelEditing, int ilaId)
        {
            var certifyingBodies = await _certifyingBodyService.GetCertifyingBodiesAsync(isLevelEditing);
            var ila = (await _iLAService.FindWithIncludeAsync(r => r.Id == ilaId, new string[] { "ILACertificationLinks.Certification.CertifyingBody", "ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement" })).FirstOrDefault();
            List<CertifyingBodyWithSubRequirementsVM> certifyingBodyWithSubsVm = new List<CertifyingBodyWithSubRequirementsVM>();

            foreach (var certifyingBody in certifyingBodies)
            {
                var firstMatchingCertification = certifyingBody.Certifications.FirstOrDefault();
                var certificationSubRequirements = firstMatchingCertification.CertificationSubRequirements.Select(sub => new SubRequirementVM
                {
                    SubRequirementId = sub.Id,
                    ReqHour = 0,
                    ReqName = sub.ReqName
                }).ToList();

                // Patch existing ilaCert and ilaCertSubrequirement values based on arbitrarily choosing first ILACertification for the current CertifyingBody, if an ILACertification exists
                var firstMatchingILALink = ila.ILACertificationLinks.FirstOrDefault(link => link.Certification.CertifyingBodyId == certifyingBody.Id);
                bool isIncludeSimulation = firstMatchingILALink?.IsIncludeSimulation ?? false;
                bool isEmergencyOpHours = firstMatchingILALink?.IsEmergencyOpHours ?? false;
                bool isPartialCreditHours = firstMatchingILALink?.IsPartialCreditHours ?? false;
                double? cehHours = firstMatchingILALink?.CEHHours ?? 0;
                if (firstMatchingILALink != null)
                {
                    foreach (var certificationSubRequirement in certificationSubRequirements)
                    {
                        var matchingILACertificationSubRequirement = firstMatchingILALink.ILACertificationSubRequirementLink.FirstOrDefault(icsl => icsl.CertificationSubRequirement.ReqName == certificationSubRequirement.ReqName);
                        certificationSubRequirement.ReqHour = matchingILACertificationSubRequirement != null ? matchingILACertificationSubRequirement.ReqHour : certificationSubRequirement.ReqHour;
                    }
                }

                var certifyingBodyConsistencyResults = ila.ILACertificationConsistencyResults;

                certifyingBody.Certifications = null;
                foreach(var cr in certifyingBodyConsistencyResults)
                {
                    cr.CertifyingBody = null;
                }

                certifyingBodyWithSubsVm.Add(
                    new CertifyingBodyWithSubRequirementsVM(
                        certifyingBody,
                        certificationSubRequirements,
                        isIncludeSimulation,
                        isEmergencyOpHours,
                        isPartialCreditHours,
                        cehHours,
                        certifyingBodyConsistencyResults
                    )
                );
            }
            return certifyingBodyWithSubsVm;
        }

        public async Task<List<SubRequirementVM>> GetCertifyingBodiesWithSubRequirementsAsync(bool isLevelEditing)
        {
            var certifyingBodies = await _certifyingBodyService.GetCertifyingBodiesAsync(isLevelEditing);

            var allSubRequirements = new List<SubRequirementVM>();

            foreach (var certifyingBody in certifyingBodies)
            {
                var firstMatchingCertification = certifyingBody.Certifications.FirstOrDefault();

                var certificationSubRequirements = firstMatchingCertification?.CertificationSubRequirements
                    .Select(sub => new SubRequirementVM
                    {
                        SubRequirementId = sub.Id,
                        ReqHour = 0,
                        ReqName = sub.ReqName
                    }).ToList();

                if (certificationSubRequirements != null && certificationSubRequirements.Any())
                {
                    allSubRequirements.AddRange(certificationSubRequirements);
                }

                certifyingBody.Certifications = null;
            }

            return allSubRequirements;
        }

    }
}
