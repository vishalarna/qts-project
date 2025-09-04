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
    public class EmployeeControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;
        public EmployeeControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }
        public static IEnumerable<object[]> Data_EmployeeController_GetEmployeeProcedureReviewsAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_EmployeeController_GetSelfRegAvailableCoursesAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsEmployeeUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_EmployeeController_GetSelfRegDroppedCoursesAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }
        public static IEnumerable<object[]> Data_EmployeeController_GetSelfRegApprovedCoursesAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }
        public static IEnumerable<object[]> Data_EmployeeController_GetSelfRegDeniedCoursesAsync()
        {
            var data = new List<object[]>();
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }
        public static IEnumerable<object[]> Data_EmployeeController_GetEmployeePendingTaskRequalificationAsync()
        {
            var data = new List<object[]>();

            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_EmployeeController_GetCompletedTaskRequalificationAsync()
        {
            var data = new List<object[]>();

            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser,true, HttpStatusCode.OK });

            return data;
        }
        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetEmployeeProcedureReviewsAsync))]
        public async void EmployeeController_GetEmployeeProcedureReviewsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/procedureReviewEmp/procedureReviews";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetSelfRegAvailableCoursesAsync))]
        public async void EmployeeController_GetSelfRegAvailableCoursesAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/selfreg/available";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetSelfRegDroppedCoursesAsync))]
        public async void EmployeeController_GetSelfRegDroppedCoursesAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/selfreg/dropped";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetSelfRegApprovedCoursesAsync))]
        public async void EmployeeController_GetSelfRegApprovedCoursesAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/selfreg/approved";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetSelfRegDeniedCoursesAsync))]
        public async void EmployeeController_GetSelfRegDeniedCoursesAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/selfreg/denied";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetEmployeePendingTaskRequalificationAsync))]
        public async void EmployeeController_GetEmployeePendingTaskRequalificationAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/empTaskQualification/pending";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_EmployeeController_GetCompletedTaskRequalificationAsync))]
        public async void EmployeeController_GetCompletedTaskRequalificationAsync(ClaimsBuilderOptions options, string username, bool isEvaluator, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/empTaskQualification/completed/isEvaluator/{isEvaluator}";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }
    }
}
