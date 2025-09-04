using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_RR_LinkRepository : Common.Repository<Procedure_RR_Link>, IProcedure_RR_LinkRepository
    {
        public Procedure_RR_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
