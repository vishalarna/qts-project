using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Meta_ILAMembers_Link : Entity
    {
        public int MetaILAID { get; set; }

        public int ILAID { get; set; }

        public int? MetaILAConfigPublishOptionID { get; set; }

        public int SequenceNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual MetaILA MetaILA { get; set; }

        public virtual MetaILAConfigurationPublishOption MetaILAConfigurationPublishOption { get; set; }   

        public Meta_ILAMembers_Link(int ilaId, int metaIlaId, int? metaILAConfigPublishOptionID, int sequenceNumber, DateTime? startDate)
        {
            MetaILAID = metaIlaId;
            ILAID = ilaId;
            MetaILAConfigPublishOptionID = metaILAConfigPublishOptionID;
            SequenceNumber = sequenceNumber;
            StartDate = startDate;
        }

        public Meta_ILAMembers_Link()
        {
        }

        public override void Delete()
        {
            Deleted = true;
            AddDomainEvent(new Domain.Events.Core.OnMetaILA_MemberLink_Deleted(this));
        }
    }
}
