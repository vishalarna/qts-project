using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_PreRequisite_Link : Entity
    {
        public int ILAId { get; set; }

        public int PreRequisiteId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual ILA PreRequisite { get; set; }

        public ILA_PreRequisite_Link(ILA ila, ILA preRequisite)
        {
            ILA = ila;
            PreRequisite = preRequisite;
            ILAId = ila.Id;
            PreRequisiteId = preRequisite.Id;
        }

        public ILA_PreRequisite_Link()
        {
        }
    }
}
