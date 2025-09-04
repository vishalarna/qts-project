using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class ProcedureReview_EmployeeRepository : Common.Repository<ProcedureReview_Employee>, IProcedureReview_EmployeeRepository
    {

        public ProcedureReview_EmployeeRepository(QTDContext qtdContext)
            : base(qtdContext)
        {

        }
    }
}
