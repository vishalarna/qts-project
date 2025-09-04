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
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.Certification_History;
using ICertificationDomainService = QTD2.Domain.Interfaces.Service.Core.ICertificationService;
using ICertificationHistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ICertification_HistoryService;

namespace QTD2.Application.Services.Shared
{
    public class CertificationHistoryService : Interfaces.Services.Shared.ICertificationHistoryService
    {
        private readonly ICertificationHistoryDomainService _certHistoryService;
        private readonly ICertificationDomainService _certService;
        private readonly IStringLocalizer<CertificationHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;


        public CertificationHistoryService(ICertificationHistoryDomainService certHistoryService, IStringLocalizer<CertificationHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager, ICertificationDomainService certService)
        {
            _certHistoryService = certHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _certService = certService;
        }


        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _certHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["CertHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _certHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<Certification_History> CreateAsync(Certification_HistoryCreateOptions options)
        {
            var hist = new Certification_History(options.CertId, options.EffectiveDate, options.Notes);

            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            var validationResult = await _certHistoryService.AddAsync(hist);
            if (validationResult.IsValid)
            {
                return hist;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }

        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var hist = await _certHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _certHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<List<Certification_History>> GetAllCertCatHistories()
        {
            var hist = (await _certHistoryService.AllAsync()).ToList();
            return hist;
        }


        public async Task<List<CertificationLatestActivityVM>> GetHistoryAsync(bool getLatest)
        {
            var certs = await _certService.AllQuery().IgnoreQueryFilters().ToListAsync();
            var history = await _certHistoryService.AllQuery().ToListAsync();
            var users = await _userManager.Users.ToListAsync();

            var latestactivity = from c in certs
                                 join h in history on c.Id equals h.CertId
                                 join u in _userManager.Users on h.CreatedBy equals u.Email
                                 select new CertificationLatestActivityVM
                                 {
                                     CertId = h.Id,
                                     Name = c.Name,
                                     CertAcronym = c.CertAcronym,
                                     ActivityDesc = h.Notes,
                                     CreatedBy = u.Email,
                                     CreatedDate = h.CreatedDate,
                                 };

            if (getLatest)
            {
                latestactivity = latestactivity.Take(5).ToList();
            }

            return latestactivity.OrderByDescending(x => x.CreatedDate).ToList();
        }


        public async Task<Certification_History> GetCertCatHistory(int id)
        {
            var hist = await _certHistoryService.GetAsync(id);
            return hist;
        }


        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _certHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _certHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<Certification_History> UpdateAsync(int id, Certification_HistoryCreateOptions options)
        {
            var hist = await _certHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, Certification_CategoryHistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    hist.Notes = options.Notes;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _certHistoryService.UpdateAsync(hist);
                    if (validationResult.IsValid)
                    {
                        return hist;
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
        }












    }
}
