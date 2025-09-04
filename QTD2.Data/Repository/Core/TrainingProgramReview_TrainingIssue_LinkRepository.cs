using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TrainingProgramReview_TrainingIssue_LinkRepository : Common.Repository<TrainingProgramReview_TrainingIssue_Link>, ITrainingProgramReview_TrainingIssue_LinkRepository
    {
        public TrainingProgramReview_TrainingIssue_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
