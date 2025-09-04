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
    public class IDP_ReviewStatusRepository : Common.Repository<IDP_ReviewStatus>, IIDP_ReviewStatusRepository
    {
        public IDP_ReviewStatusRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
