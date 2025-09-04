using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using Xunit;
using QTD2.Domain.Entities.Core;
using QTD2.Domain;
using System.Security.Claims;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
   public class EmpDashboardControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;
        public EmpDashboardControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_EmpDashboardController_GetDashboardStatisticsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_EmpDashboardController_CheckCourseAvailabilityForSelfRegestrationAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }


        [Theory]
        [MemberData(nameof(Data_EmpDashboardController_GetDashboardStatisticsAsync))]
        public async void EmpDashboardController_GetDashboardStatisticsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/dashboard/statistics";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }


        [Theory]
        [MemberData(nameof(Data_EmpDashboardController_CheckCourseAvailabilityForSelfRegestrationAsync))]
        public async void EmpDashboardController_CheckCourseAvailabilityForSelfRegestrationAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/dashboard/checkCourseAvailability";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }
    }
}
