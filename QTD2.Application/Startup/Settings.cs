using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QTD2.Infrastructure.JWT;
using Serilog;
using Serilog.Formatting.Json;

using QTD2.Infrastructure.Reports;

namespace QTD2.Application.Startup
{
    public static class Settings
    {
        private static readonly string _domainSection = "ApplicationSettings:Domains";
        private static readonly string _jwtSection = "Jwt";
        private static readonly string _reportSection = "Reports";
        private static readonly string _errorhandlerSection = "ErrorHandlerSettings";

        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Application.Settings.DomainSettings>(configuration.GetSection(_domainSection));
            services.Configure<JWTSettings>(configuration.GetSection(_jwtSection));
            services.Configure<ReportSettings>(configuration.GetSection(_reportSection));
            services.Configure<Infrastructure.Notification.Settings.NotificationSettings>(options => configuration.GetSection("NotificationSettings").Bind(options));
            services.Configure<Infrastructure.Scorm.Settings.ScormServerSettings>(options => configuration.GetSection("Scorm").Bind(options));
            services.Configure<Infrastructure.QTD2Auth.Settings.QTDAuthSettings>(options => configuration.GetSection("QtdAuthSettings").Bind(options));
            services.Configure<Infrastructure.QTDSettings.QTDSettings>(options => configuration.GetSection("QTDSettings").Bind(options));
            services.Configure<Infrastructure.Hashing.Settings.HashIdOptions>(options => configuration.GetSection("HashIds").Bind(options));
            services.Configure<Infrastructure.Notification.Settings.EmailNotifications>(options => configuration.GetSection("EmailNotifications").Bind(options));
            services.Configure<Infrastructure.ErrorHandler.ErrorHandlerSettings>(options => configuration.GetSection("ErrorHandlerSettings").Bind(options));
            services.Configure<Infrastructure.QTD2Auth.Settings.CustomMfaSettings>(options => configuration.GetSection("CustomMfaSettings").Bind(options));
            services.Configure<Infrastructure.QTD2Auth.Settings.ResetPasswordSettings>(options => configuration.GetSection("ResetPasswordSettings").Bind(options));
            services.Configure<Infrastructure.Database.Settings.DBMigratorSettings>(options => configuration.GetSection("DBMigratorSettings").Bind(options));
        }
    }
}
