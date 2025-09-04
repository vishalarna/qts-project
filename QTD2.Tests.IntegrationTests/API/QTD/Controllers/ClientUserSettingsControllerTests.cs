using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using Xunit;
using QTD2.Domain.Entities.Core;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class ClientUserSettingsControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;

        public ClientUserSettingsControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_ClientUserSettingsController_GetDashboardSettingsAsync()
        {
            var data = new List<object[]>();

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientUserSettingsController_UpdateDashboardSettingsAsync()
        {
            var data = new List<object[]>();

            var updateOptions = new Infrastructure.Model.ClientUserSettings.CustomizeDashboardUpdateOptions()
            {
                Updates = new List<Infrastructure.Model.ClientUserSettings.CustomDashboardSettingOption>()
             {
                 new Infrastructure.Model.ClientUserSettings.CustomDashboardSettingOption()
                 {
                     Enable = true,
                     Settings = "Tasks Overview"
                 },
                 new Infrastructure.Model.ClientUserSettings.CustomDashboardSettingOption()
                 {
                     Disable = true,
                     Settings = "Employees Overview"
                 },
             }
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, updateOptions, HttpStatusCode.OK });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_ClientUserSettingsController_GetDashboardSettingsAsync))]
        public async System.Threading.Tasks.Task ClientUserSettingsController_GetDashboardSettingsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/clientUserSettings/dashboard";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ClientUserSettingsController_UpdateDashboardSettingsAsync))]
        public async System.Threading.Tasks.Task ClientUserSettingsController_UpdateDashboardSettingsAsync(
                                                                    ClaimsBuilderOptions options,
                                                                    string username,
                                                                    Infrastructure.Model.ClientUserSettings.CustomizeDashboardUpdateOptions updateOptions,
                                                                    HttpStatusCode httpStatusCode)
        {
            string url = $"/clientUserSettings/dashboard";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }
    }
}
