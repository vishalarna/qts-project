using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class PositionTestData
    {
        public static List<Position> GetAll()
        {
            return new List<Position>()
            {
                p1(),
                p2()
            };
        }

        static Position p1()
        {
            return new Position(1, "TPA","PT","PD","HL",true,null,System.DateTime.Now,"FileName");
        }

        static Position p2()
        {
            return new Position(2, "TPAN", "PTN", "PDN", "HLN", false, null, System.DateTime.Now, "FileName");
        }
    }
}
