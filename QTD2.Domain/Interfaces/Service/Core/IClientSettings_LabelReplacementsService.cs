using QTD2.Domain.Entities.Core;
using QTD2.Domain.Validation;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClientSettings_LabelReplacementsService : Common.IService<ClientSettings_LabelReplacement>
    {
        System.Threading.Tasks.Task<List<ClientSettings_LabelReplacement>> GetLabelReplacementAsync();
        System.Threading.Tasks.Task<ClientSettings_LabelReplacement> GetByDefaultLabel(string defaultLabel);
    }
}
