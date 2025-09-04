using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.MetaILAConfigurationPublishOption;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IMetaILAConfigurationPublishOptionService
    {
        public Task<List<MetaILAConfigurationPublishOption>> GetAsync();

        public Task<MetaILAConfigurationPublishOption> GetAsync(int id);

        public Task<MetaILAConfigurationPublishOption> CreateAsync(MetaILAConfigurationPublishOptionCreateOptions options);

        public Task<MetaILAConfigurationPublishOption> UpdateAsync(int id, MetaILAConfigurationPublishOptionUpdateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task ActiveAsync(int id);

        public System.Threading.Tasks.Task InActiveAsync(int id);
    }
}
