using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;
using System;
using Xunit;

namespace QTD2.Tests.UnitTests.API.QTD.Controllers
{
    public partial class IssuingAuthoritiesControllerTests
    {
        //[Theory, MemberData(nameof(IssuingAuthoritiesControllerTests.Create_StatusCodeCorrect))]
        public async void IssuingAuthoritiesController_Create_StatusCodeCorrect(Procedure_IssuingAuthorityCreateOptions options, int statusCode)
        {
            if ((statusCode >= 200 && statusCode <= 299) || statusCode == 303)
            {
                var result = await _controller.CreateAsync(options) as ObjectResult;
                Assert.Equal(result.StatusCode, statusCode);
            }

            //this can be better by determining the type of exception thrown
            else
                await Assert.ThrowsAnyAsync<Exception>(() => _controller.CreateAsync(options));
        }
    }
}
