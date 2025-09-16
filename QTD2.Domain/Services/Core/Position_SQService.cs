using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace QTD2.Domain.Services.Core
{
    public class Position_SQService : Common.Service<Positions_SQ>, IPosition_SQService
    {
        public Position_SQService(IPositions_SQRepository positionSqRepository, IPositions_SQValidation positionSqValidation)
            : base(positionSqRepository, positionSqValidation)
        {
        }

        public async Task<List<Positions_SQ>> GetPositionsSQByEOIdAsync(int eoId)
        {
            var positions_SQs = await FindAsync(r => r.EOId == eoId);
            return positions_SQs.ToList();
        }
    }
}
