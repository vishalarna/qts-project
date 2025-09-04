using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TrainingIssue_DriverTypeRepository : Common.Repository<TrainingIssue_DriverType>, ITrainingIssue_DriverTypeRepository
    {
        public TrainingIssue_DriverTypeRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
