using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;

namespace QTD2.Domain.Services.Core
{
  public  class Employee_ScormUploadService : Common.Service<Employee_ScormUpload>, IEmployee_ScormUploadService
    {
        public Employee_ScormUploadService(IEmployee_ScormUploadRepository repository, IEmployee_ScormUploadValidation validation)
            : base(repository, validation)
        {
        }
    }
}
