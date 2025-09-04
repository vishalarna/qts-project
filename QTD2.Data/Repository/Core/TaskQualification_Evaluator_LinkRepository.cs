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
    public class TaskQualification_Evaluator_LinkRepository : Common.Repository<TaskQualification_Evaluator_Link>, ITaskQualification_Evaluator_LinkRepository
    {
        public TaskQualification_Evaluator_LinkRepository(QTDContext context)
            : base(context)
        {
        }
    }
}
