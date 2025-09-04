using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ToolCategory_StatusHistoryRepository : Common.Repository<ToolCategory_StatusHistory>, IToolCategory_StatusHistoryRepository
    {

        public ToolCategory_StatusHistoryRepository(QTDContext qtdContext)
            : base(qtdContext)
        {

        }
    }
}
