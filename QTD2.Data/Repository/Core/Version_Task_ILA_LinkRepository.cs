using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class Version_Task_ILA_LinkRepository : Common.Repository<Version_Task_ILA_Link>, IVersion_Task_ILA_LinkRepository
    {
        public Version_Task_ILA_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
