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
    public class Version_Task_TrainingGroupRepository : Common.Repository<Version_Task_TrainingGroup>, IVersion_Task_TrainingGroupRepository
    {
        public Version_Task_TrainingGroupRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
