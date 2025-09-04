using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Meta_ILAMembers_LinkRepository : Common.Repository<Meta_ILAMembers_Link>, IMeta_ILAMembers_LinkRepository
    {
        public Meta_ILAMembers_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}