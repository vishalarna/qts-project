using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class Procedure_ILA_LinkRepository : Common.Repository<Procedure_ILA_Link>, IProcedure_ILA_LinkRepository
    {
        public Procedure_ILA_LinkRepository(QTDContext qtdContext)
            : base(qtdContext)
        {
        }
    }
}
