using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Core
{
    public class TaskReQualificationEmp_SignOffRepository : Common.Repository<TaskReQualificationEmp_SignOff>, ITaskReQualificationEmp_SignOffRepository
    {

        public TaskReQualificationEmp_SignOffRepository(QTDContext qtdContext)
            : base(qtdContext)
        {

        }
    }
}
