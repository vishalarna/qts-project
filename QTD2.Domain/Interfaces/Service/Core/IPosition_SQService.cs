using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;


namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IPosition_SQService : IService<Positions_SQ>
    {
        public System.Threading.Tasks.Task<List<Positions_SQ>> GetPositionsSQByEOIdAsync(int eoId);
    }
}
