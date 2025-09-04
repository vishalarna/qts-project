using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;

namespace QTD2.Data.Repository.Core
{
    public class TrainingTopic_CategoryRepository : Common.Repository<TrainingTopic_Category>, ITrainingTopic_CategoryRepository
    {
        public TrainingTopic_CategoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
