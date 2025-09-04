using QTD2.Domain;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class ReportSkeletonControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;
        public ReportSkeletonControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_ReportsController_GetReportSkeletonAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser,1, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ReportsController_GetReportSkeletonsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }
        [Theory]
        [MemberData(nameof(Data_ReportsController_GetReportSkeletonAsync))]
        public async void ReportsController_GetReportSkeletonAsync(ClaimsBuilderOptions options, string username, int reportSkeletonId, HttpStatusCode httpStatusCode)
        {
            string url = $"reportSkeletons/{reportSkeletonId}";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ReportsController_GetReportSkeletonsAsync))]
        public async void ReportsController_GetReportSkeletonsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/reportSkeletons";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

    }
}
