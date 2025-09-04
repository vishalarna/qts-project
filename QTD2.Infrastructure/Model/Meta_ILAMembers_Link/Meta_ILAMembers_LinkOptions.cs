using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Meta_ILAMembers_Link
{
    public class Meta_ILAMembers_LinkOptions
    {
        public int MetaILAID { get; set; }

        public int ILAID { get; set; }

        public int? MetaILAConfigPublishOptionID { get; set; }

        public int SequenceNumber { get; set; }
        public DateTime? StartDate { get; set; }
    }

    public class Meta_ILAMembers_ListOptions
    {
        public List<Meta_ILAMembers_LinkOptions> IlaMetaILAMembers { get; set; } = new List<Meta_ILAMembers_LinkOptions>();
    }
}
