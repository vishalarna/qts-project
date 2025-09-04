using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QTD2.Domain.Services.Core
{
    public class PositionHistoryService : Common.Service<Position_History>, IPositionHistoryService
    {
        public PositionHistoryService(IPositionHistoryRepository positionhistoryRepository, IPositionHistoryValidation positionhistoryValidation)
            : base(positionhistoryRepository, positionhistoryValidation)
        {
        }

    }
}
