using QTD2.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Identity.Settings;
using QTD2.Test.Data.Infrastructure.Model.CertifyingBody;
using QTD2.Tests.IntegrationTests.Testing.Base;
using QTD2.Tests.IntegrationTests.Testing.Fixures;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using QTD2.Application.Interfaces.Services.Authentication;
using Xunit;

namespace QTD2.Tests.IntegrationTests.Notifications
{
    [Collection("Authentication Collection")]
    public class AuthenticationNotificationTests : AuthenticationControllerBaseClass
    {
        protected INotificationService _notificationService { get { return (_fixture as AuthenticationFixture).NotificationService; } }
        protected Application.Interfaces.Services.Shared.IUserService _userService { get { return (_fixture as AuthenticationFixture).UserService; } }


        public AuthenticationNotificationTests(AuthenticationFixture authenticationFixture) : base(authenticationFixture)
        {
        }

        public static IEnumerable<object[]> NotificationTestData()
        {
            var data = new List<object[]>();

            data.Add(new object[] { "qtd@qualitytrainingsystems.com" });

            return data;
        }

        [Theory]
        [MemberData(nameof(NotificationTestData))]
        public async Task TwoFANotification(string email)
        {
            await _fixture.StartupAsync();
            await _notificationService.Send2FANotificationAsync(email, "some verification token");
        }

        [Theory]
        [MemberData(nameof(NotificationTestData))]
        public async Task SendResetPasswordNotificationAsync(string email)
        {
            await _fixture.StartupAsync();
            await _notificationService.SendResetPasswordNotificationAsync(email, "https://www.qts.com/ForgotPassword?token=sometoken");
        }
    }
}
