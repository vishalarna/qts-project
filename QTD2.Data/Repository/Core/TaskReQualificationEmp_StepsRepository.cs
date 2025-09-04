using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TaskReQualificationEmp_StepsRepository : Common.Repository<TaskReQualificationEmp_Steps>, ITaskReQualificationEmp_StepsRepository
    {

        public TaskReQualificationEmp_StepsRepository(QTDContext qtdContext)
            : base(qtdContext)
        {

        }
    }
}
