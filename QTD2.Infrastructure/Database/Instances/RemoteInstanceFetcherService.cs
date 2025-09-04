using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Database
{
    public class RemoteInstanceFetcherService : Interfaces.IInstanceFetcher
    {
        HttpClients.QtdAuthenticationService _qtdAuthenticationService;
        private Domain.Entities.Authentication.InstanceSetting _currentInstanceSetting;

        public RemoteInstanceFetcherService(HttpClients.QtdAuthenticationService qtdAuthenticationService)
        {
            _qtdAuthenticationService = qtdAuthenticationService;
        }

		public async Task<List<InstanceSetting>> GetAllInstancesAsync()
        {
            return await _qtdAuthenticationService.Instances.GetAsync();
        }

        public async Task<List<InstanceSetting>> GetAllActiveInstancesAsync()
        {
            return await _qtdAuthenticationService.Instances.GetActiveAsync();
        }

        public async Task<InstanceSetting> GetInstanceSettingAsync(string name)
        {
            if (_currentInstanceSetting == null)
            {
                var setting = await _qtdAuthenticationService.Instances.GetInstanceSettingsAsync(name);
                _currentInstanceSetting = setting;
            }

            return _currentInstanceSetting;
        }
    }
}
