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
    public class  Location_HistoryRepository  : Common.Repository<Location_History>, ILocation_HistoryRepository
    {

        public Location_HistoryRepository(QTDContext context)
            :base (context)
        {

        }
    }
}
