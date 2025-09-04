using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class TrainingIssue_DriverSubTypeService : Common.Service<TrainingIssue_DriverSubType>, ITrainingIssue_DriverSubTypeService
    {
        public TrainingIssue_DriverSubTypeService(ITrainingIssue_DriverSubTypeRepository repository, ITrainingIssue_DriverSubType_Validation validation)
          : base(repository, validation)
        {
        }
    }
}
