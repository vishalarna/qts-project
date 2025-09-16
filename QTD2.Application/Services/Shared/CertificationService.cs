using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.CertifyingBody;
using QTD2.Infrastructure.Model.Certification_History;
using ICertifyingBodyDomainService = QTD2.Domain.Interfaces.Service.Core.ICertifyingBodyService;
using ICertificationDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationService;
using ICertificationLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationLinkService;
using ICertificationSubRequirementLinkDomainService = QTD2.Domain.Interfaces.Service.Core.IILACertificationSubRequirementLinkService;
using IILADomainService = QTD2.Domain.Interfaces.Service.Core.IILAService;
using QTD2.Infrastructure.Model.CertificationSubRequirement;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class CertificationService : ICertificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<CertificationService> _localizer;
        private readonly ICertificationDomainService _certificationService;
        private readonly ICertifyingBodyDomainService _certifyingBodyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICertificationSubRequirementService _certificationSubRequirementService;
        private readonly ICertificationLinkDomainService _certificationLinkDomainService;
        private readonly ICertificationSubRequirementLinkDomainService _ilaCertificationSubRequirementLinkService;
        private readonly IILADomainService _ilaService;

        public CertificationService(ICertificationLinkDomainService certificationLinkDomainService,
                    ICertificationSubRequirementLinkDomainService certificationSubRequirementLinkDomainService,
                    IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<CertificationService> localizer, ICertificationDomainService certificationService, UserManager<AppUser> userManager, ICertifyingBodyDomainService certifyingBodyService, ICertificationSubRequirementService certificationSubRequirementDomainService, IILADomainService ilaService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _certificationService = certificationService;
            _userManager = userManager;
            _certifyingBodyService = certifyingBodyService;
            _certificationSubRequirementService = certificationSubRequirementDomainService;
            _certificationLinkDomainService = certificationLinkDomainService;
            _ilaCertificationSubRequirementLinkService = certificationSubRequirementLinkDomainService;
            _ilaService = ilaService;
        }


        public async System.Threading.Tasks.Task ActiveAsync(Certification_HistoryCreateOptions options)
        {
            if (options != null && options.certIds.Count() > 0)
            {
                foreach (var cert in options.certIds)
                {
                    var obj = await GetAsync(cert);
                    obj.Activate();

                    var validationResult = await _certificationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }
                }

            }
            else
            {
                throw new QTDServerException("Certification Ids not found");
            }

        }


        public async Task<Certification> CreateAsync(CertificationCreateOptions options)
        {
            var obj = (await _certificationService.FindAsync(x => x.CertifyingBodyId == options.CertifyingBodyId && x.Name == options.Name)).FirstOrDefault();
            if (obj == null)
            {
                //obj = new Certification(options.Name, options.CertifyingBodyId, options.CertNumber, options.CertDesc, options.CertMemberNum, options.CertifiedDate, 
                //                                 options.RenewalDate, options.ExpirationDate, options.RenewalInterval, options.AllowRollover, options.AdditionalHours);

                obj = new Certification(options.CertifyingBodyId, options.CertAcronym, options.Name, options.CertDesc, options.RenewalTimeFrame, options.RenewalInterval,
                                        options.CreditHrsReq, options.CreditHrs, options.CertSubReq, " ", 0, options.CertMemberNum,
                                        options.CertifiedDate, options.RenewalDate, options.ExpirationDate, options.AllowRolloverHours, options.AdditionalHours, options.EffectiveDate);

            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["RecordAlreadyExists"].Value);
            }


            obj.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.CreatedDate = DateTime.Now;
            var validationResult = await _certificationService.AddAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }

        }

        public async System.Threading.Tasks.Task DeleteAsync(Certification_HistoryCreateOptions options)
        {
            if (options != null && options.certIds.Count() > 0)
            {
                foreach (var cert in options.certIds)
                {
                    var obj = await GetAsync(cert);
                    obj.Delete();

                    var validationResult = await _certificationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }

                }

            }
            else
            {
                throw new QTDServerException("Cert Ids not found");
            }
        }

        public async Task<List<Certification>> GetAsync()
        {
            var obj_list = await _certificationService.AllWithIncludeAsync(new[] { "CertifyingBody" } );
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, AuthorizationOperations.Read).Result.Succeeded);
            return obj_list.OrderBy(o => o.Name)?.ToList();
        }

        public async Task<bool> SaveIlaCertificationLinkAsync(ILACertificationVM data)
        {
            var ilaCertificate = (await _certificationLinkDomainService.FindWithIncludeAsync(x => x.ILAId == data.IlaId && x.CertificationId == data.CertificationId, new string[] { "ILACertificationSubRequirementLink" }))?.FirstOrDefault();
            //var ila = await _ilaService.FindQuery(x => x.Id == data.IlaId).FirstOrDefaultAsync();
            if (ilaCertificate == null)
            {
                ilaCertificate = new ILACertificationLink();
                ilaCertificate.ILAId = data.IlaId;
                ilaCertificate.CertificationId = data.CertificationId;

                ilaCertificate.IsPartialCreditHours = data.IsPartialCreditHours;
                ilaCertificate.IsEmergencyOpHours = data.IsEmergencyOpHours;
                ilaCertificate.IsIncludeSimulation = data.IsIncludeSimulation;
                ilaCertificate.CEHHours = data.CEHHours;
                var result = await _certificationLinkDomainService.AddAsync(ilaCertificate);
                foreach (var item in data.CertificationSubRequirements)
                {
                    var subRequirementLink = new ILACertificationSubRequirementLink();
                    subRequirementLink.ReqHour = item.ReqHour;
                    subRequirementLink.ILACertificationLinkId = ilaCertificate.Id;
                    subRequirementLink.CertificationSubRequirementId = item.SubRequirementId;
                    var subReqResult = await _ilaCertificationSubRequirementLinkService.AddAsync(subRequirementLink);
                }
            }
            else
            {
                ilaCertificate.ILAId = data.IlaId;
                ilaCertificate.CertificationId = data.CertificationId;
                //ilaCertificate.TotalHours = data.TotalHours;
                ilaCertificate.IsPartialCreditHours = data.IsPartialCreditHours;
                ilaCertificate.IsEmergencyOpHours = data.IsEmergencyOpHours;
                ilaCertificate.IsIncludeSimulation = data.IsIncludeSimulation;
                ilaCertificate.CEHHours = data.CEHHours;
                var result = await _certificationLinkDomainService.UpdateAsync(ilaCertificate);
                foreach (var item in data.CertificationSubRequirements)
                {
                    var suvReqLinkData = (await _ilaCertificationSubRequirementLinkService.FindAsync(x => x.CertificationSubRequirementId == item.SubRequirementId && x.ILACertificationLinkId == ilaCertificate.Id))?.FirstOrDefault();
                    if (suvReqLinkData != null)
                    {
                        suvReqLinkData.ReqHour = item.ReqHour;
                        suvReqLinkData.ILACertificationLinkId = ilaCertificate.Id;
                        suvReqLinkData.CertificationSubRequirementId = item.SubRequirementId;
                        var subReqResult = await _ilaCertificationSubRequirementLinkService.UpdateAsync(suvReqLinkData);
                    }
                    else
                    {
                        var subRequirementLink = new ILACertificationSubRequirementLink();
                        subRequirementLink.ReqHour = item.ReqHour;
                        subRequirementLink.ILACertificationLinkId = ilaCertificate.Id;
                        subRequirementLink.CertificationSubRequirementId = item.SubRequirementId;
                        var subReqResult = await _ilaCertificationSubRequirementLinkService.AddAsync(subRequirementLink);
                    }
                }
            }
            return true;
        }
        public async Task<bool> RemoveLinkWithIlaDataAsync(int Certid, int ilaId)
        {

            var certificationLink = (await _certificationLinkDomainService.FindAsync(x => x.ILAId == ilaId && x.CertificationId == Certid)).FirstOrDefault();
            if (certificationLink != null)
            {
                var tbd = await _ilaCertificationSubRequirementLinkService.FindAsync(x => x.ILACertificationLinkId == certificationLink.Id);
                foreach (var item in tbd)
                {
                    item.Delete();
                    await _ilaCertificationSubRequirementLinkService.UpdateAsync(item);
                }
                certificationLink.Delete();
                var result = await _certificationLinkDomainService.UpdateAsync(certificationLink);
                if (result.IsValid)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<ILACertificationVM> GetWithIlaDataAsync(int Certid, int ilaId)
        {
            var result = new ILACertificationVM();
            var certification = await _certificationService.GetWithIncludeAsync(Certid, new string[] { "CertifyingBody", "CertificationSubRequirements" });
            var ila = (await _ilaService.FindWithIncludeAsync(x => x.Id == ilaId, new string[] { "ILACertificationLinks.ILACertificationSubRequirementLink" })).FirstOrDefault();
            if (certification != null)
            {
                var ilaCertificationLink = ila.ILACertificationLinks.FirstOrDefault(x => x.CertificationId == certification.Id);
                result.CertificationId = Certid;
                result.IsNerc = certification.CertifyingBody.IsNERC ?? true;
                result.IlaId = ilaId;
                result.Name = certification.Name;
                result.IsAlreadySaved = ilaCertificationLink == null ? false : true;
                result.IsEmergencyOpHours = ilaCertificationLink?.IsEmergencyOpHours ?? false;
                result.IsPartialCreditHours = ilaCertificationLink?.IsPartialCreditHours ?? false;
                result.IsIncludeSimulation = ilaCertificationLink?.IsIncludeSimulation ?? false;
                result.TotalHours = ila.TotalTrainingHours ?? 0.0d;
                result.CEHHours = ilaCertificationLink?.CEHHours ?? 0;
                result.CertificationSubRequirements = new List<SubRequirementVM>();
                foreach (var certificationSubRequirement in certification.CertificationSubRequirements)
                {
                    var ilaCertificationSubRequirementLink = ilaCertificationLink?.ILACertificationSubRequirementLink.FirstOrDefault(icsl => icsl.CertificationSubRequirementId == certificationSubRequirement.Id);
                    var subRequirement = new SubRequirementVM();
                    subRequirement.ReqName = certificationSubRequirement.ReqName;
                    subRequirement.SubRequirementId = certificationSubRequirement.Id;
                    subRequirement.ReqHour = ilaCertificationSubRequirementLink?.ReqHour ?? 0;
                    result.CertificationSubRequirements.Add(subRequirement);
                }
                return result;
            }
            else
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }
        }

        public async Task<Certification> GetAsync(int id)
        {
            var obj = await _certificationService.GetWithIncludeAsync(id, new string[] { "EmployeeCertifications" });

            // var obj = await _certificationService.GetAsync(id);
            if (obj != null)
            {

                return obj;

            }
            else
            {
                throw new QTDServerException( _localizer["RecordNotFound"].Value);
            }
        }


        public async System.Threading.Tasks.Task InActiveAsync(Certification_HistoryCreateOptions options)
        {
            if (options != null && options.certIds.Count() > 0)
            {
                foreach (var cert in options.certIds)
                {
                    var obj = await GetAsync(cert);
                    obj.Deactivate();

                    var validationResult = await _certificationService.UpdateAsync(obj);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));

                    }
                }
            }
            else
            {
                throw new QTDServerException("Cert Ids not found");
            }
        }


        public async Task<Certification> UpdateAsync(int id, CertificationCreateOptions options)
        {
            var obj = await GetAsync(id);

            obj.Name = options.Name;
            obj.CertifyingBodyId = options.CertifyingBodyId;
            obj.CertAcronym = options.CertAcronym;
            obj.CertDesc = options.CertDesc;
            obj.RenewalTimeFrame = options.RenewalTimeFrame;
            obj.RenewalInterval = options.RenewalInterval;
            obj.CreditHrsReq = obj.CreditHrsReq;
            obj.CreditHrs = options.CreditHrs;
            obj.CertSubReq = options.CertSubReq;
            obj.CertSubReqName = " ";
            obj.CertSubReqHours = 0;
            obj.CertMemberNum = options.CertMemberNum;
            obj.CertifiedDate = options.CertifiedDate;
            obj.RenewalDate = options.RenewalDate;
            obj.ExpirationDate = options.ExpirationDate;
            obj.AllowRolloverHours = options.AllowRolloverHours;
            obj.AdditionalHours = options.AdditionalHours;
            obj.EffectiveDate = options.EffectiveDate;

            obj.ModifiedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            obj.ModifiedDate = DateTime.Now;

            var validationResult = await _certificationService.UpdateAsync(obj);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
            else
            {
                return obj;
            }

        }


        public async Task<int> getCount()
        {
            var result = await _certificationService.AllQueryWitDeletedCount();
            return result;
        }

        public async Task<List<string>> getLinkedCertifications(int ilaId)
        {
            var data = await _certificationLinkDomainService.FindWithIncludeAsync(x => x.ILAId == ilaId, new string[] { "Certification" });
            return data.Select(x => x.Certification.Name).ToList();
        }


        public async Task<CertificationStatsVM> GetStatsCount()
        {
            var certs = await _certificationService.AllQuery().Select(x => x.Id).ToListAsync();
            var categories = await _certifyingBodyService.AllQuery().Select(x => x.Id).ToListAsync();

            var stats = new CertificationStatsVM()
            {
                CertificationActive = await _certificationService.GetCount(x => x.Active == true),
                CertificationInactive = await _certificationService.GetCount(x => x.Active == false),
                CertIAActive = await _certifyingBodyService.GetCount(x => x.Active == true),
                CertIAInactive = await _certifyingBodyService.GetCount(x => x.Active == false),

            };

            return stats;
        }

        public async Task<List<Certification>> GetCertActiveInactive(string option)
        {
            var rrList = new List<Certification>();

            switch (option.ToLower().Trim())
            {
                case "certactive":
                    rrList = await _certificationService.FindQuery(x => x.Active == true).Select(s => new Certification
                    {
                        Id = s.Id,
                        CertAcronym = s.CertAcronym,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToListAsync();
                    break;
                case "certinactive":
                    rrList = await _certificationService.FindQuery(x => x.Active == false).Select(s => new Certification
                    {
                        Id = s.Id,
                        CertAcronym = s.CertAcronym,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToListAsync();
                    break;
            }

            return rrList;

        }

        public async Task<List<CertifyingBody>> GetCatActiveInactive(string option)
        {
            var rrList = new List<CertifyingBody>();

            switch (option.ToLower().Trim())
            {
                case "catactive":
                    rrList = await _certifyingBodyService.FindQuery(x => x.Active == true).Select(s => new CertifyingBody
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToListAsync();
                    break;
                case "catinactive":
                    rrList = await _certifyingBodyService.FindQuery(x => x.Active == false).Select(s => new CertifyingBody
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }).OrderBy(o => o.Name).ToListAsync();
                    break;
            }

            return rrList;
        }

        public async Task<List<Certification>> GetNercCertificatesAsync()
        {
            var nercCerts = await _certificationService.GetNercCertificatesAsync();
            return nercCerts;
        }

        public async Task<List<SubRequirementVM>> GetSubRequirementsByCertIdAsync(int certId)
        {
            var certification = (await _certificationService.GetCertificationSubRequirementByCertificationIdAsync(certId)).FirstOrDefault();

            if (certification == null)
            {
                throw new QTDServerException(_localizer["RecordNotFound"].Value);
            }

            return certification.CertificationSubRequirements
                .Select(sub => new SubRequirementVM
                {
                    SubRequirementId = sub.Id,
                    ReqName = sub.ReqName,
                    ReqHour = 0
                }).ToList();
        }

    }
}
