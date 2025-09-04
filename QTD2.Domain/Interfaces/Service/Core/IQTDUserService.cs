using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;

using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IQTDUserService : IService<QTDUser>
    {
        System.Threading.Tasks.Task<List<QTDUser>> GetAllActive();
        System.Threading.Tasks.Task<QTDUser> GetQTDUserByUsername(string username);
    }
}
