using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Database.Settings;
using QTD2.Infrastructure.Jobs.Interfaces;

namespace QTD2.Application.Jobs.Startup
{
    public class QTDAuthDatabaseSetup : IJob
    {
        private readonly Data.QTDAuthenticationContext _authContext;
        private readonly IWebHostEnvironment _env;
        private readonly bool _runAtStartup;

        public bool RunAtStartup => _runAtStartup;

        public QTDAuthDatabaseSetup(IWebHostEnvironment env, Data.QTDAuthenticationContext authContext, IOptions<DBMigratorSettings>  dBMigratorSettings)
        {
            _env = env;
            _authContext = authContext;
            _runAtStartup = dBMigratorSettings.Value.RunMigrationsAtStartup;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    _authContext.Database.Migrate();
                });
            }
            catch (Exception e)
            {

            }

        }
    }
}
