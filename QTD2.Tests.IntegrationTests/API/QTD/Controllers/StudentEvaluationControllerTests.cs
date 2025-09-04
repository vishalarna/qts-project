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
    public class StudentEvaluationControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;

        public StudentEvaluationControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_StudentEvaluationController_GetEmployeeEvaluationsAsync()
        {
            var data = new List<object[]>();

            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_StudentEvaluationController_StartEmployeeEvaluationsAsync()
        {
            var data = new List<object[]>();

            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser,1, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_StudentEvaluationController_CompleteEmployeeEvaluationsAsync()
        {
            var data = new List<object[]>();
            
            var nClaims = new List<Claim>();
            nClaims.Add(new Claim(CustomClaimTypes.EmployeeId, true.ToString()));
            nClaims.Add(new Claim(CustomClaimTypes.IsQTDUser, "true"));
            nClaims.Add(new Claim(CustomClaimTypes.InstanceName, "QTD2"));

            var updateOptions = new Infrastructure.Model.StudentEvaluationForm.ClassSchedule_Evaluation_RosterOptions()
            {
              employeeId = 1,
              classId = 1,
              evaluationId = 1
            };

            data.Add(new object[] { new ClaimsBuilderOptions(nClaims), FullRightsUser, updateOptions, HttpStatusCode.OK });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_StudentEvaluationController_GetEmployeeEvaluationsAsync))]
        public async void StudentEvaluationController_GetEmployeeEvaluationsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/student-evaluations";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_StudentEvaluationController_StartEmployeeEvaluationsAsync))]
        public async void StudentEvaluationController_StartEmployeeEvaluationsAsync(ClaimsBuilderOptions options, string username, int evaluationId, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/student-evaluations/start/{evaluationId}";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_StudentEvaluationController_CompleteEmployeeEvaluationsAsync))]
        public async void StudentEvaluationController_CompleteEmployeeEvaluationsAsync(ClaimsBuilderOptions options, string username, Infrastructure.Model.StudentEvaluationForm.ClassSchedule_Evaluation_RosterOptions updateOptions, HttpStatusCode httpStatusCode)
        {
            string url = $"/emp/student-evaluations/complete";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }

    }
}
