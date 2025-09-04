using System;
using Microsoft.Extensions.Configuration;

namespace QTD2.Infrastructure.Configuration
{
    public class SettingsConfigHelper
    {
        private static SettingsConfigHelper _appSettings;

        public string appSettingValue { get; set; }

        public static string AppSetting(string key)
        {
            _appSettings = GetCurrentSettings(key);
            return _appSettings.appSettingValue;
        }

        public SettingsConfigHelper(IConfiguration config, string key)
        {
            appSettingValue = config.GetValue<string>(key);
        }

        // Get a valued stored in the appsettings.
        // Pass in a key like TestArea:TestKey to get TestValue
        public static SettingsConfigHelper GetCurrentSettings(string key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new SettingsConfigHelper(configuration.GetSection("ApplicationSettings"), key);

            return settings;
        }

        public static string QTDDomain
        {
            get { return AppSetting("Domains:QTD"); }
        }

        public static string EMPDomain
        {
            get { return AppSetting("Domains:EMP"); }
        }

        public static string AdminDomain
        {
            get { return AppSetting("Domains:Admin"); }
        }
    }
}
