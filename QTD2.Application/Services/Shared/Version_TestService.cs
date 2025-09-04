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
using IVersionTestDomainService = QTD2.Domain.Interfaces.Service.Core.IVersion_TestService;

namespace QTD2.Application.Services.Shared
{
    public class Version_TestService : IVersion_TestService
    {
        private readonly IVersionTestDomainService _version_testService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<Version_Test> _localizer;
        private readonly UserManager<AppUser> _userManager;

        public Version_TestService(
            IVersionTestDomainService version_testService,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService,
            IStringLocalizer<Version_Test> localizer,
            UserManager<AppUser> userManager)
        {
            _version_testService = version_testService;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task VersionTestAsync(Test test,int state = 0)
        {
            var inUseTest = await _version_testService.FindQuery(x => x.IsInUse == true && x.TestId == test.Id).FirstOrDefaultAsync();
            if( inUseTest != null )
            {
                inUseTest.IsInUse = false;
                await _version_testService.UpdateAsync(inUseTest);
            }
            var number = (await _version_testService.FindQueryWithDeleted(x => x.TestId == test.Id).OrderBy(x => x.Id).LastOrDefaultAsync())?.Version_Number ?? "0.0";
            var nextNum = double.Parse(number) + 1;
            number = nextNum + ".0";
            Version_Test vTest = new Version_Test(test, number,state);
            vTest.IsInUse = true;
            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, vTest, Version_TestOperations.Create);
            var validationResult = await _version_testService.AddAsync(vTest);
            if(!validationResult.IsValid) 
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }
    }
}
