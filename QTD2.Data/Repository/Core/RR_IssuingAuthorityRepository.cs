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
    public class RR_IssuingAuthorityRepository : Common.Repository<RR_IssuingAuthority>, IRR_IssuingAuthorityRepository
    {
        public RR_IssuingAuthorityRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
