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
    public class Location_CategoryHistoryRepository : Common.Repository<Location_CategoryHistory>, ILocation_CategoryHistoryRepository
    {

        public Location_CategoryHistoryRepository (QTDContext context)
            :base (context)
        {

        }
    }
}
