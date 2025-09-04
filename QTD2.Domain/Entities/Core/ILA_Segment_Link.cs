using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Segment_Link : Entity
    {
        public int ILAId { get; set; }

        public int SegmentId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Segment Segment { get; set; }

        public int? DisplayOrder { get; set; }

        public ILA_Segment_Link(ILA iLA, Segment segment)
        {
            ILAId = iLA.Id;
            SegmentId = segment.Id;
            ILA = iLA;
            Segment = segment;
        }

        public ILA_Segment_Link()
        {
        }


        public override T Copy<T>(string createdBy)
        {
            var copy = base.Copy<T>(createdBy) as ILA_Segment_Link;

            copy.ILAId = 0;
            copy.ILA = null;

            copy.SegmentId = 0;
            copy.Segment = this.Segment.Copy<Segment>(createdBy);

            return (T)(object)copy;
        }
    }
}
