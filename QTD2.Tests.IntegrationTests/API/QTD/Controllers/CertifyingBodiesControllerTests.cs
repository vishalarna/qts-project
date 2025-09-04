using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Model.CertifyingBody;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class CertifyingBodiesControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;

        public CertifyingBodiesControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_CertifyingBodiesController_Get()
        {
            var data = new List<object[]>();

            data.Add(new object[] { null, null, HttpStatusCode.Unauthorized });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_CertifyingBodiesController_Post()
        {
            var data = new List<object[]>();

            data.Add(new object[] { null, null, CertifyingBodyUpdateOptionsTestData.TheNoDuplicateGuy(), HttpStatusCode.Unauthorized });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, CertifyingBodyUpdateOptionsTestData.Empty(), HttpStatusCode.UnprocessableEntity });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, CertifyingBodyUpdateOptionsTestData.NameTooLong(), HttpStatusCode.UnprocessableEntity });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, CertifyingBodyUpdateOptionsTestData.NERC(), HttpStatusCode.RedirectMethod });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, CertifyingBodyUpdateOptionsTestData.TheNoDuplicateGuy(), HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_CertifyingBodiesController_Put()
        {
            var data = new List<object[]>();

            data.Add(new object[] { null, null, HttpStatusCode.Unauthorized });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_CertifyingBodiesController_Delete()
        {
            var data = new List<object[]>();

            data.Add(new object[] { null, null, HttpStatusCode.Unauthorized });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        //[Theory]
        //[MemberData(nameof(Data_CertifyingBodiesController_Get))]
        public async void CertifyingBodiesControllerController_Get(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = "/CertifyingBodies";
            await TestGetAsync(url, username, options, httpStatusCode);
        }

        //[Theory]
        //[MemberData(nameof(Data_CertifyingBodiesController_Post))]
        public async void CertifyingBodiesControllerController_Post(ClaimsBuilderOptions options, string username, object model, HttpStatusCode httpStatusCode)
        {
            string url = "/CertifyingBodies";
            await TestPostAsync(url, username, options, model, httpStatusCode);
        }

        //[Theory]
        //[MemberData(nameof(Data_CertifyingBodiesController_Put))]
        public async void CertifyingBodiesControllerController_Put(ClaimsBuilderOptions options, string username, string certifyingBodyName, object model, HttpStatusCode httpStatusCode)
        {
            string url = $"/CertifyingBodies/{certifyingBodyName}";
            await TestPutAsync(url, username, options, model, httpStatusCode);
        }

        //[Theory]
        //[MemberData(nameof(Data_CertifyingBodiesController_Delete))]
        public async void CertifyingBodiesControllerController_Delete(ClaimsBuilderOptions options, string username, string certifyingBodyName, HttpStatusCode httpStatusCode)
        {
            string url = $"/CertifyingBodies/{certifyingBodyName}";
            await TestDeleteAsync(url, username, options, httpStatusCode);
        }
    }
}
