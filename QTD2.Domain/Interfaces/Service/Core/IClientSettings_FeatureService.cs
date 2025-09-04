using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClientSettings_FeatureService : Common.IService<ClientSettings_Feature>
    {
        System.Threading.Tasks.Task<List<ClientSettings_Feature>> GetAllFeaturesAsync();
    }
}
