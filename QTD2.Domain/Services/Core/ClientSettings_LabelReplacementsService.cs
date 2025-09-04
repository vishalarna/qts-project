using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClientSettings_LabelReplacementsService : Common.Service<ClientSettings_LabelReplacement>, IClientSettings_LabelReplacementsService
    {

        public ClientSettings_LabelReplacementsService(IClientSettings_LabelReplacementsRepository repository, IClientUserSettings_LabelReplacementValidation validation)
              : base(repository, validation)
        {
        }

        public async Task<ClientSettings_LabelReplacement> GetByDefaultLabel(string defaultLabel)
        {
            return (await FindAsync(r => r.DefaultLabel.ToUpper() == defaultLabel.ToUpper())).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<ClientSettings_LabelReplacement>> GetLabelReplacementAsync()
        {
            return (await AllAsync()).ToList();
        }
    }
}
