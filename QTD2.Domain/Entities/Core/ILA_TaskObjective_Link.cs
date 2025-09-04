using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_TaskObjective_Link : Entity
    {
        public int ILAId { get; set; }

        public int TaskId { get; set; }

        public bool UseForTQ { get; set; }
        public int SequenceNumber { get; set; }

        public int ILAObjectiveOrder { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Task Task { get; set; }

        public ILA_TaskObjective_Link(ILA ila, Task task)
        {
            ILA = ila;
            Task = task;
            ILAId = ila.Id;
            TaskId = task.Id;
        }

        public ILA_TaskObjective_Link(ILA ila, Task task,int sequenceNumber)
        {
            ILAId = ila.Id;
            TaskId = task.Id;
            SequenceNumber = sequenceNumber;
        }

        public ILA_TaskObjective_Link()
        {
        }

        public void UpdateSequenceNumber(int sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }

        public void UpdateOrder(int currentOrder)
        {
            ILAObjectiveOrder = currentOrder;
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnILA_TaskObjectiveLink_Unlinking(this));
        }
    }
}
