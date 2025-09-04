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
    public class TrainingGroup_CategoryRepository : Common.Repository<TrainingGroup_Category>, ITrainingGroup_CategoryRepository
    {
        public TrainingGroup_CategoryRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
