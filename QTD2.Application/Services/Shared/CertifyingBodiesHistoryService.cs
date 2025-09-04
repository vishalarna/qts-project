using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Domain.Exceptions;
using QTD2.Infrastructure.Model.CertifyingBody_History;
using ICertifyingBody_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ICertifyingBody_HistoryService;

namespace QTD2.Application.Services.Shared
{
    public class CertifyingBodiesHistoryService : Interfaces.Services.Shared.ICertifyingBodies_HistoryService
    {
        private readonly ICertifyingBody_HistoryDomainService _certbodyHistoryService;
        private readonly IStringLocalizer<CertifyingBodiesHistoryService> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<AppUser> _userManager;


        public CertifyingBodiesHistoryService(ICertifyingBody_HistoryDomainService certbodytHistoryService, IStringLocalizer<CertifyingBodiesHistoryService> localizer, IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, UserManager<AppUser> userManager)
        {
            _certbodyHistoryService = certbodytHistoryService;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }


        public async System.Threading.Tasks.Task ActiveAsync(int id)
        {
            var hist = await _certbodyHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException(_localizer["CertifyingBodyHistoryNotFound"]);
            }
            else
            {
                hist.Activate();
                await _certbodyHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<CertifyingBody_History> CreateAsync(CertifyingBody_HistoryCreateOptions options)
        {
            var hist = new CertifyingBody_History(options.CertifyingBodyID, options.EffectiveDate, options.Notes);

            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            hist.CreatedDate = DateTime.Now;
            hist.Notes = options.Notes;
            var validationResult = await _certbodyHistoryService.AddAsync(hist);
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
            var hist = await _certbodyHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertifyingBodyHistoryNotFound"]);
            }
            else
            {
                hist.Delete();
                await _certbodyHistoryService.UpdateAsync(hist);
            }
        }

        public async Task<List<CertifyingBody_History>> GetAllCertCatHistories()
        {
            var hist = (await _certbodyHistoryService.AllAsync()).ToList();
            return hist;
        }

        public async Task<CertifyingBody_History> GetCertCatHistory(int id)
        {
            var hist = await _certbodyHistoryService.GetAsync(id);
            return hist;
        }

        public async System.Threading.Tasks.Task InActiveAsync(int id)
        {
            var hist = await _certbodyHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertifyingBodyHistoryNotFound"]);
            }
            else
            {
                hist.Deactivate();
                await _certbodyHistoryService.UpdateAsync(hist);
            }
        }


        public async Task<CertifyingBody_History> UpdateAsync(int id, CertifyingBody_HistoryCreateOptions options)
        {
            var hist = await _certbodyHistoryService.GetAsync(id);
            if (hist == null)
            {
                throw new QTDServerException( _localizer["CertifyingBodyHistoryNotFound"]);
            }
            else
            {
                var histResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, hist, CertifyingBody_HistoryOperations.Update);
                if (histResult.Succeeded)
                {
                    hist.CertifyingBodyId = options.CertifyingBodyID;
                    hist.EffectiveDate = options.EffectiveDate;
                    var validationResult = await _certbodyHistoryService.UpdateAsync(hist);
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
