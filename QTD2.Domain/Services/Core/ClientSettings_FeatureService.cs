using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClientSettings_FeatureService : Common.Service<ClientSettings_Feature>, IClientSettings_FeatureService
    {
        public ClientSettings_FeatureService(IClientSettings_FeatureRepository repository, IClientSettings_FeatureValidation validation)
             : base(repository, validation)
        {
        }
        public async Task<List<ClientSettings_Feature>> GetAllFeaturesAsync()
        {
            return (await AllAsync()).ToList();
        }
    }
}
