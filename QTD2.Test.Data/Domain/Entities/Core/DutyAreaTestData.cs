using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class DutyAreaTestData
    {
        public static List<DutyArea> GetAll()
        {
            return new List<DutyArea>()
            {
                DA1(),
                DA2WOLetter(),
            };
        }

        static DutyArea DA1()
        {
            return new DutyArea("TO","Transmission Operations", "§", 1,System.DateTime.Now,"Test Reason");
        }

        static DutyArea DA2WOLetter()
        {
            return new DutyArea("MC","Machine Controllers", null, 2, System.DateTime.Now, "Test Reason");
        }
    }
}
