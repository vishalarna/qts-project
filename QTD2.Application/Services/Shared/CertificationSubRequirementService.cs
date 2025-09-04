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
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_History;
using ICertSubReqDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationSubRequirementService;
using QTD2.Infrastructure.Model.CertificationSubRequirement;

namespace QTD2.Application.Services.Shared
{
    public class CertificationSubRequirementService : Interfaces.Services.Shared.ICertificationSubRequirementService
    {

        private readonly ICertSubReqDomainService _certSubReqDomainService;
        private readonly IStringLocalizer<CertificationSubRequirementService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly Certification _cert;

        public CertificationSubRequirementService(ICertSubReqDomainService certSubReqDomainService, IStringLocalizer<CertificationSubRequirementService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _certSubReqDomainService = certSubReqDomainService;
            _cert = new Certification();
        }

        public async System.Threading.Tasks.Task CreateAsync(CertificationSubRequirementCreateOptions options)
        {
            foreach (var (v1, v2) in options.ReqName.Zip(options.ReqHour, (x, y) => (x, y)))
                //foreach(var hour in options.ReqHour)
            {
                var certSubService = new CertificationSubRequirement(options.CertificationId, v1,(int)v2);
                    certSubService.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                    certSubService.CreatedDate = DateTime.Now;
                    var validationResult = await _certSubReqDomainService.AddAsync(certSubService);
                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
            }
           
        }

        public async Task<List<CertificationSubRequirement>> GetAsync(int id)
        {
            var data = await _certSubReqDomainService.FindQuery(x => x.CertificationId == id).ToListAsync();
            return data;
        }

        public async Task<List<CertificationSubRequirement>> UpdateAsync(CertificationSubRequirementCreateOptions options)
        {
            var existingRecords = await GetAsync(options.CertificationId);
            var combinedData = options.ReqName.Zip(options.ReqHour, (reqName, reqHour) => new { reqName, reqHour }).ToList();
            var newIds = options.CertificationSubRequirementsIds ?? Array.Empty<int>();

            foreach (var existingRecord in existingRecords)
            {
                if (!newIds.Contains(existingRecord.Id))
                {
                    existingRecord.Deleted = true;
                    await _certSubReqDomainService.UpdateAsync(existingRecord);
                }
            }

            int existingCount = existingRecords.Where(r => !r.Deleted).Count();
            int newCount = combinedData.Count;

            for (int i = 0; i < Math.Min(existingCount, newCount); i++)
            {
                existingRecords[i].ReqName = combinedData[i].reqName;
                existingRecords[i].ReqHour = (int)combinedData[i].reqHour;

                var validationResult = await _certSubReqDomainService.UpdateAsync(existingRecords[i]);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(string.Join(',', validationResult.Errors));
                }
            }

            if (newCount > existingCount)
            {
                for (int i = existingCount; i < newCount; i++)
                {
                    var newOption = new CertificationSubRequirementCreateOptions
                    {
                        CertificationId = options.CertificationId,
                        ReqName = new string[] { combinedData[i].reqName },
                        ReqHour = new float[] { combinedData[i].reqHour }
                    };
                    await CreateAsync(newOption);
                }
            }
            return existingRecords.ToList();
        }
    }
}
