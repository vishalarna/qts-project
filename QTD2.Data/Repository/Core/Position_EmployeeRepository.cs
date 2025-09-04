using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class Position_EmployeeRepository : Common.Repository<Position_Employee>, IPosition_EmployeeRepository
    {
        public Position_EmployeeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
