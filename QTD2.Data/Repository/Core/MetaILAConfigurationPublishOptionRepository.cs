using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class MetaILAConfigurationPublishOptionRepository : Common.Repository<MetaILAConfigurationPublishOption>, IMetaILAConfigurationPublishOptionRepository
    {
        public MetaILAConfigurationPublishOptionRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
