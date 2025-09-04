using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using Xunit;
using QTD2.Domain.Entities.Core;
using System.Security.Claims;
using QTD2.Domain;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class ILAControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;

        public ILAControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_ILAController_RegisterEmployeeToCbtAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, 1, 1, HttpStatusCode.OK });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_ILAController_RegisterEmployeeToCbtAsync))]
        public async void ILAController_RegisterEmployeeToCbtAsync(ClaimsBuilderOptions options, string username, int cbtId, int employeeId, HttpStatusCode httpStatusCode)
        {
            string url = $"/classSchedule/{cbtId}/employees/{employeeId}";
            await TestPostAsync(url, username, options, null, httpStatusCode);
        }
    }
}
