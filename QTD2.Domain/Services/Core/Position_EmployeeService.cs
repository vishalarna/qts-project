using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Common;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class Position_EmployeeService : Common.Service<Position_Employee>, IPosition_EmployeeService
    {
        public Position_EmployeeService(IPosition_EmployeeRepository repository, IPosition_EmployeeValidation validation)
            : base(repository, validation)
        {
        }
    }
}
