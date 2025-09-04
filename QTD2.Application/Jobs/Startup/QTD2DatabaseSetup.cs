using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QTD2.Infrastructure.Jobs.Interfaces;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.HttpClients;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Database.Settings;

namespace QTD2.Application.Jobs.Startup
{
    public class QTD2DatabaseSetup : IJob
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDatabaseManager _dbManager;
        private readonly QtdAuthenticationService _qtdAuthService;
        private readonly IInstanceFetcher _instanceFetcher;
        private ILogger<QTD2DatabaseSetup> _logger;
        private readonly bool _runAtStartup;

        public QTD2DatabaseSetup(
            IWebHostEnvironment env,
            IDatabaseManager dbManager,
            QtdAuthenticationService qtdAuthService,
            IInstanceFetcher instanceFetcher,
            ILogger<QTD2DatabaseSetup> logger, IOptions<DBMigratorSettings> dBMigratorSettings)
        {
            _env = env;
            _dbManager = dbManager;
            _qtdAuthService = qtdAuthService;
            _instanceFetcher = instanceFetcher;
            _logger = logger;
            _runAtStartup = dBMigratorSettings.Value.RunMigrationsAtStartup;
        }

        public bool RunAtStartup => _runAtStartup;

        public async Task ExecuteAsync()
        {
            _logger.LogInformation($"QTD2Database Set up for environment {_env.EnvironmentName} beginning");

            var instances = await _instanceFetcher.GetAllActiveInstancesAsync();
            foreach (var instance in instances)
            {
                try
                {
                    _logger.LogInformation($"QTD2Database Set up for {instance.DatabaseName} beginning");
                    await _dbManager.MigrateDatabaseAsync(instance.DatabaseName);
                }
                catch (Exception e)
                {
                    _logger.LogError($"QTD2Database Set up for {instance.DatabaseName} failed {e}", e);
                }
            }
        }
    }
}
