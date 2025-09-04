using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Test_History;
using ITest_HistoryDomainService = QTD2.Domain.Interfaces.Service.Core.ITest_HistoryService;

namespace QTD2.Application.Services.Shared
{
    public class Test_HistoryService : ITest_HistoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Test_History> _localizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITest_HistoryDomainService _test_HistoryService;

        public Test_HistoryService(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<Test_History> localizer,
            UserManager<AppUser> userManager,
            ITest_HistoryDomainService test_HistoryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
            _test_HistoryService = test_HistoryService;
        }

        public async System.Threading.Tasks.Task CreateTestHistoryAsync(Test_HistoryCreateOptions options)
        {
            Test_History hist = new Test_History();
            hist.TestId = options.TestId;
            hist.ChangeNotes = options.ChangeNotes;
            hist.EffectiveDate = options.EffectiveDate;
            hist.CreatedDate = DateTime.Now;
            hist.CreatedBy = (await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name)).Id;
            var validationResult = await _test_HistoryService.AddAsync(hist);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }
    }
}
