using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_Task_Link : Entity
    {
        public int ProcedureId { get; set; }

        public int TaskId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Task Task { get; set; }

        public Procedure_Task_Link()
        {
        }

        public Procedure_Task_Link(Procedure procedure, Task task)
        {
            ProcedureId = procedure.Id;
            TaskId = task.Id;
            Procedure = procedure;
            Task = task;
        }
    }
}
