using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Data.Repository.Core
{
    public class Employee_ScormUploadRepository : Common.Repository<Employee_ScormUpload>, IEmployee_ScormUploadRepository
    {
        public Employee_ScormUploadRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
