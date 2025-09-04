using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IAdminMessage_QTDUserService : IService<AdminMessage_QTDUser>
    {
        Task<List<AdminMessage_QTDUser>> GetAdminMessageByUserIdAsync(int userId);
        Task<AdminMessage_QTDUser> GetAdminMessageAsync(int AdminMessage_QTDUserId);
        Task<List<AdminMessage_QTDUser>> GetExistingAdminMessageByUserIdAsync(int userId);

    }
}
