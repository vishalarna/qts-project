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
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeCertificationHistory;
using IEmployeeCertificationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeCertificationHistoryService;
using IEmployeeCertificationDomainService = QTD2.Domain.Interfaces.Service.Core.IEmployeeCertificationService;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class EmployeeCertificationHistoryService : Application.Interfaces.Services.Shared.IEmployeeCertificationHistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<EmployeeCertificationHistoryService> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeCertificationHistoryDomainService _employeeCertificationHistoryService;
        private readonly IEmployeeCertificationDomainService _employeeCertificationDomainService;

        public EmployeeCertificationHistoryService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IStringLocalizer<EmployeeCertificationHistoryService> localizer, UserManager<AppUser> userManager, IEmployeeCertificationHistoryDomainService employeeCertificationHistoryService,
            IEmployeeCertificationDomainService employeeCertificationDomainService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _employeeCertificationHistoryService = employeeCertificationHistoryService;
            _employeeCertificationDomainService = employeeCertificationDomainService;
        }

        public async Task<EmployeeCertifictaionHistory> CreateAsync(EmployeeCertificationHistoryCreateOptions options)
        {
            var obj = new EmployeeCertifictaionHistory(options.EmployeeCertificationId, (options.ChangeEffectiveDate != default(DateOnly) ? options.ChangeEffectiveDate : DateOnly.FromDateTime(DateTime.UtcNow)), options.ChangeNotes);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, Proc_IssuingAuthority_HistoryOperations.Create);
            if (result.Succeeded)
            {
                var userName = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
                obj.Create(userName);
                obj.ExpirationDate = options.ExpirationDate.GetValueOrDefault();
                obj.IssueDate = options.RenewalDate.GetValueOrDefault();
                obj.DRADate = options.RenewalDate.GetValueOrDefault();
                obj.CertificationNumber = options.CertificationNumber;
                var validationResult = await _employeeCertificationHistoryService.AddAsync(obj);
                if (validationResult.IsValid)
                {
                    
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async System.Threading.Tasks.Task DeleteHistAsync(int certLinkId)
        {
            var certHist = await _employeeCertificationHistoryService.FindQuery(x => x.Id == certLinkId).FirstOrDefaultAsync();

            if(certHist != null)
            {
                certHist.Delete();
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(certHist);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",",validationResult.Errors)]);
                }
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync(int id, EmployeeCertificateUpdateOptions options)
        {
            var certHist = await _employeeCertificationHistoryService.FindQuery(x => x.Id == id).FirstOrDefaultAsync();
            if(certHist != null)
            {
                certHist.IssueDate = options.IssueDate;
                certHist.DRADate = options.RenewalDate.GetValueOrDefault();   
                certHist.ExpirationDate = options.ExpirationDate.GetValueOrDefault();
                certHist.CertificationNumber = options.CertificationNumber;
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(certHist);
                if (!validationResult.IsValid)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: _localizer[string.Join(",",validationResult.Errors)]);
                }
            }
            else
            {
                throw new BadHttpRequestException(message: _localizer["Certification History Entry Not Found"]);
            }
        }

        public async Task<EmployeeCertifictaionHistory> ActiveAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeCertificationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Activate();
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<EmployeeCertifictaionHistory> DeactivateAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeCertificationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public async Task<EmployeeCertifictaionHistory> DeleteAsync(int id)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeCertificationHistoryOperations.Delete);
            if (result.Succeeded)
            {
                obj.Delete();
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }

        public List<EmployeeCertifictaionHistory> GetAsync()
        {
            var obj = _employeeCertificationHistoryService.AllQuery().Where(r => !r.Deleted);
            var data = obj.ToList().Where(x => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, x, EmployeeCertificationHistoryOperations.Read).Result.Succeeded);
            return data.ToList();
        }

        public async Task<EmployeeCertifictaionHistory> GetAsync(int id)
        {
            var obj = await _employeeCertificationHistoryService.GetAsync(id);
            if (obj != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeCertificationHistoryOperations.Read);
                if (result.Succeeded)
                {
                    return obj;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException(_localizer["IssuingAuthorityStatusHistoryNotFound"]);
            }
        }

        public async Task<EmployeeCertifictaionHistory> UpdateAsync(int id, EmployeeCertificationHistoryCreateOptions options)
        {
            var obj = await GetAsync(id);
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, EmployeeCertificationHistoryOperations.Update);
            if (result.Succeeded)
            {
                // Change the update logic as required
                obj.ChangeNotes = options.ChangeNotes;
                obj.ChangeEffectiveDate = options.ChangeEffectiveDate;
                var validationResult = await _employeeCertificationHistoryService.UpdateAsync(obj);
                if (validationResult.IsValid)
                {
                    return obj;
                }
                else
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                }
            }
            else
            {
                throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
            }
        }
        public async Task<List<EmployeeCertifictaionHistory>> GetCertificationWithEmpCertificationHistory(int empCertId)
        {
            var empCertificationHistory =await _employeeCertificationHistoryService.FindAsync(x => x.EmployeeCertificationId==empCertId);
            return empCertificationHistory.ToList();
        }

        public async System.Threading.Tasks.Task DeleteBulkHistoryAsync(EmployeeCertificationHistoryDeleteOptions options)
        {
            foreach (var historyId in options.EmployeeCertificationHistoryIds)
            {
                var historyRecord = await GetAsync(historyId);
                if (historyRecord == null) continue;

                var authResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, historyRecord, ReportOperations.Delete);

                if (authResult.Succeeded)
                {
                    historyRecord.Delete();
                    var validationResult = await _employeeCertificationHistoryService.UpdateAsync(historyRecord);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
                else
                {
                    throw new UnauthorizedAccessException("OperationNotAllowed");
                }
            }
        }
    }
}
