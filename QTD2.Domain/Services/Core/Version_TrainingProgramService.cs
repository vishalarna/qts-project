using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
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
    public class Version_TrainingProgramService : Common.Service<Version_TrainingProgram>, IVersion_TrainingProgramService
    {
        public Version_TrainingProgramService(IVersion_TrainingProgramRepository repository, IVersion_TrainingProgram validation)
            : base(repository, validation)
        {
        }
    }
}
