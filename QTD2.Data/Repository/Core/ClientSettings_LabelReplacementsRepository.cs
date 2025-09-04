using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class ClientSettings_LabelReplacementsRepository : Common.Repository<ClientSettings_LabelReplacement>, IClientSettings_LabelReplacementsRepository
    {
        public ClientSettings_LabelReplacementsRepository(QTDContext context)
           : base(context)
        {
        }
    }
}
