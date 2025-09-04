using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_EnablingObjective_Link : Entity
    {
        public int ILAId { get; set; }

        public int EnablingObjectiveId { get; set; }

        public int ILAObjectiveOrder { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public ILA_EnablingObjective_Link(ILA ila, EnablingObjective enablingObjective)
        {
            ILA = ila;
            EnablingObjective = enablingObjective;
            ILAId = ila.Id;
            EnablingObjectiveId = enablingObjective.Id;
        }

        public ILA_EnablingObjective_Link()
        {
        }

        public void UpdateOrder(int currentOrder)
        {
            ILAObjectiveOrder = currentOrder;
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnILA_EnablingObjectiveLink_Unlinking(this));
        }
    }
}
