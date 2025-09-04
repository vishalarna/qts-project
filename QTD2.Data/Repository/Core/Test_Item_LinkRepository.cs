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
    public class Test_Item_LinkRepository : Common.Repository<Test_Item_Link>, ITest_Item_LinkRepository
    {
        public Test_Item_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
