using QTD2.Infrastructure.Identity.ClaimsBuilders;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;
using QTD2.Infrastructure.Database.Interfaces;

namespace QTD2.Infrastructure.Identity
{
    public class AuthClaimsBuilderFactory : IClaimsBuilderFactory
    {
        private readonly IDbContextBuilder _dbContextBuilder;
        private readonly Domain.Interfaces.Service.Authentication.IInstanceSettingService _instanceSettingsService;
        private readonly Domain.Interfaces.Service.Authentication.IInstanceService _instanceService;
        private readonly Metrics.Interfaces.IMetricLogger _metricLogger;

        public AuthClaimsBuilderFactory(
                IDbContextBuilder dbContextBuilder, 
                Domain.Interfaces.Service.Authentication.IInstanceSettingService instanceSettingsService,
                Domain.Interfaces.Service.Authentication.IInstanceService instanceService,
                Metrics.Interfaces.IMetricLogger metricLogger)
        {
            _dbContextBuilder = dbContextBuilder;
            _instanceSettingsService = instanceSettingsService;
            _instanceService = instanceService;
            _metricLogger = metricLogger;
        }

        public IClaimsBuilder GetBuilder(ClaimsBuilderOptions options)
        {
            if (options.IsRefreshToken)
            {
                return new RefreshClaimsBuilder();
            }

            if (options.CloneClaims != null)
            {
                return new CloneClaimsBuilder();
            }

            return new DefaultClaimsBuilder(_dbContextBuilder, _instanceSettingsService, _instanceService, _metricLogger);
        }
    }
}
