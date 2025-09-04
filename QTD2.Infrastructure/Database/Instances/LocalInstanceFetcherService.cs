using Microsoft.Extensions.Options;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Database
{
    public class LocalInstanceFetcherService : Interfaces.IInstanceFetcher
    {
        private readonly List<Settings.DbContextConfiguration> _dbContexts;
        public LocalInstanceFetcherService(IOptions<List<Settings.DbContextConfiguration>> dbContextOptions)
        {
            _dbContexts = dbContextOptions.Value;
        }

        public Task<string> GetConnectionStringAsync()
        {
            Settings.DbContextConfiguration selectedContext = new Settings.DbContextConfiguration();

            selectedContext = _dbContexts.Where(r => r.Name == DbContextNames.QTDContext_Admin).First();
            string connectionString = selectedContext.ConnectionString.Replace("{DB}", "QTD2");
            return Task.FromResult(connectionString);
        }

        public async Task<List<InstanceSetting>> GetAllInstancesAsync()
        {
            List<InstanceSetting> instances = new List<InstanceSetting>();
            instances.Add(new InstanceSetting(1, "QTD2", "1.0"));
            return instances;
        }

        public async Task<List<InstanceSetting>> GetAllActiveInstancesAsync()
        {
            return await GetAllInstancesAsync(); 
        }

        public async Task<InstanceSetting> GetInstanceSettingAsync(string name)
        {
            return new InstanceSetting(1, "QTD2", "1");
        }
	}
}
