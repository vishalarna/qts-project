using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEnablingObjective_TopicService : Common.IService<EnablingObjective_Topic>
    {
        Task<List<EnablingObjective_Topic>> GetMinimalEOTopicData();
    }
}
