using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILACustomObjective_Link : Entity
    {
        public int ILAId { get; set; }

        public int CustomObjId { get; set; }

        public int ILAObjectiveOrder { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual CustomEnablingObjective CustomEnablingObjective { get; set; }

        public ILACustomObjective_Link(ILA iLA, CustomEnablingObjective customEnablingObjective)
        {
            ILAId = iLA.Id;
            CustomObjId = customEnablingObjective.Id;
            ILA = iLA;
            CustomEnablingObjective = customEnablingObjective;
        }

        public ILACustomObjective_Link()
        {
        }

        public void UpdateOrder(int currentOrder)
        {
            ILAObjectiveOrder = currentOrder;
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnILACustomEOlink_Unlinking(this));
        }
    }
}
