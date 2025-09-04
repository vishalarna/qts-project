using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Tool_StatusHistoryRepository : Common.Repository<Tool_StatusHistory>, ITool_StatusHistoryRepository
    {
        public Tool_StatusHistoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
