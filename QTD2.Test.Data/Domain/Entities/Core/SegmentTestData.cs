using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SegmentTestData
    {
        public static List<Segment> getAll()
        {
            return new List<Segment>
            {
                s01(),s02()
            };
        }

        static Segment s01()
        {
            var segment = new Segment("first segment", 1, false, false, false, "first segment", new byte[0],true);
            segment.Set_Id(1);
            return segment;
        }

        static Segment s02()
        {
            var segment = new Segment("second segment", 1, false, false, false, "second segment", new byte[0],true);
            segment.Set_Id(2);
            return segment;
        }
    }
}
