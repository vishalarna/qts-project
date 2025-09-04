using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Model.CertifyingBody;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using Xunit;
using QTD2.Domain.Entities.Core;

namespace QTD2.Tests.IntegrationTests.API.QTD.Controllers
{
    [Collection("QTD Collection")]
    public class ClientSettingsControllerTests : QTDControllerBase
    {
        readonly QTDFixture _qtdFixture;

        public ClientSettingsControllerTests(QTDFixture qtdFixture) : base(qtdFixture)
        {

        }

        public static IEnumerable<object[]> Data_ClientSettingsController_GetNotificationsAsync()
        {
            var data = new List<object[]>();

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_PutNotificationAsync()
        {
            var data = new List<object[]>();

            var updateOptions = new Infrastructure.Model.ClientSettings.ClientSettings_NotificationUpdateOptions()
            {
                Enable = true,
               // AddRecipients = new Dictionary<int, int>() { [1] = 2 },
              //  RemoveRecipients = new Dictionary<int, int>() { [1] = 1 },
                NotificationCustomSettings = new List<Infrastructure.Model.ClientSettings.CustomSetting_Notification_Setting>()
                {

                },
                NotificationStepCustomSettings = new List<Infrastructure.Model.ClientSettings.StepCustomSetting>()
                {

                }
             //   UpdateTemplates = new Dictionary<int, string>()
              //  {
              //       [1] = "This template is updated"
               // }
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, "Emp Login", updateOptions, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_GetGeneralSettingsAsync()
        {
            var data = new List<object[]>();

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_UpdateGeneralSettingsAsync()
        {
            var data = new List<object[]>();

            var updateOptions = new Infrastructure.Model.ClientSettings.ClientSettings_GeneralSettingsUpdateOptions()
            {
               CompanyName = "Test Company Name Change"
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, updateOptions, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_GetLabelReplacementsAsync()
        {
            var data = new List<object[]>();

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_UpdateLabelReplacementsAsync()
        {
            var data = new List<object[]>();

            var updateOptions = new Infrastructure.Model.ClientSettings.ClientSettings_LabelReplacementUpdateOptions()
            {
               LabelReplacements = new List<Infrastructure.Model.ClientSettings.LabelReplacementOptions>()
               {
                   new Infrastructure.Model.ClientSettings.LabelReplacementOptions()
                   {
                       DefaultLabel = "Task",
                       LabelReplacement = "Task IT Replacement"
                   }
               }
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, updateOptions, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_GetCurrentLicenseAsync()
        {
            var data = new List<object[]>();

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, HttpStatusCode.OK });

            return data;
        }

        public static IEnumerable<object[]> Data_ClientSettingsController_UpdateLicenseAsync()
        {
            var data = new List<object[]>();

            var updateOptions = new Infrastructure.Model.ClientSettings.ClientSettings_LicenseUpdateOptions()
            {
                ClientAccountNumber = 1,
                ActivationCode = "640048518400096"
            };

            var invalid = new Infrastructure.Model.ClientSettings.ClientSettings_LicenseUpdateOptions()
            {
                ClientAccountNumber = 1,
                ActivationCode = "546123498156"
            };

            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, updateOptions, HttpStatusCode.OK });
            data.Add(new object[] { ClaimsBuilderOptionsTestData.AuthToken2FAApproved, FullRightsUser, invalid, HttpStatusCode.InternalServerError });

            return data;
        }

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_GetNotificationsAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_GetNotificationsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/notifications";
           var content =  await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_PutNotificationAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_PutNotificationAsync(
                                                                    ClaimsBuilderOptions options, 
                                                                    string username, 
                                                                    int clientId, 
                                                                    string notification, 
                                                                    Infrastructure.Model.ClientSettings.ClientSettings_NotificationUpdateOptions updateOptions,
                                                                    HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/notifications/{notification}";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }

        //General Settings

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_GetGeneralSettingsAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_GetGeneralSettingsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/general";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_UpdateGeneralSettingsAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_UpdateGeneralSettingsAsync(
                                                            ClaimsBuilderOptions options,
                                                            string username,
                                                            Infrastructure.Model.ClientSettings.ClientSettings_GeneralSettingsUpdateOptions updateOptions,
                                                            HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/general";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }


        //Label Replacements

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_GetLabelReplacementsAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_GetLabelReplacementsAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/labelReplacements";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_UpdateLabelReplacementsAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_UpdateLabelReplacementsAsync(
                                                            ClaimsBuilderOptions options,
                                                            string username,
                                                            Infrastructure.Model.ClientSettings.ClientSettings_LabelReplacementUpdateOptions updateOptions,
                                                            HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/labelReplacements";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }


        //License

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_GetCurrentLicenseAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_GetCurrentLicenseAsync(ClaimsBuilderOptions options, string username, HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/license";
            var content = await TestGetAsync(url, username, options, httpStatusCode);
        }

        [Theory]
        [MemberData(nameof(Data_ClientSettingsController_UpdateLicenseAsync))]
        public async System.Threading.Tasks.Task ClientSettingsController_UpdateLicenseAsync(
                                                            ClaimsBuilderOptions options,
                                                            string username,
                                                            Infrastructure.Model.ClientSettings.ClientSettings_LicenseUpdateOptions updateOptions,
                                                            HttpStatusCode httpStatusCode)
        {
            string url = $"/clientSettings/license";
            await TestPutAsync(url, username, options, updateOptions, httpStatusCode);
        }
    }
}
