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
    public class Procedure_Task_LinkRepository : Common.Repository<Procedure_Task_Link>, IProcedure_Task_LinkRepository
    {
        public Procedure_Task_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
