using Microsoft.AspNetCore.Mvc;
using QTD2.Infrastructure.Model.ClientUserSettings ;
using System;
using System.Text.Json;
using Xunit;

namespace QTD2.Tests.UnitTests.API.QTD.Controllers
{
    public class ClientUserSettingsTests
    {

        [Fact]
        public async void DeserilaizeTest()
        {
            string json = "{ \"settingOptions\":[{ \"settings\":\"EO Overview\",\"enable\":true,\"disable\":false},{ \"settings\":\"Procedure Overview\",\"enable\":true,\"disable\":false}]}";

            var obj = JsonSerializer.Deserialize<CustomizeDashboardUpdateOptions>(json);
        }
    }
}
