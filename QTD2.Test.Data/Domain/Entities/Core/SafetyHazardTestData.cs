using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SafetyHazardTestData
    {
        public static List<SaftyHazard> GetAll()
        {
            return new List<SaftyHazard>()
            {
                SH1(),
                SH2()
            };

        }
        static SaftyHazard SH1()
        {
            var sh = new SaftyHazard(1,"Safety Hazard Title", "","1.0",System.DateTime.Now,"Hyperlink","Set Data","TextData","Files","Image","Name");
            sh.Set_Id(1);
            return sh;
        }
        static SaftyHazard SH2()
        {
            var sh = new SaftyHazard(1, "Safety Hazard Title1", "", "1.0", System.DateTime.Now, "Hyperlink", "Set Data", "TextData", "Files", "Image", "Name");
            sh.Set_Id(2);
            return sh;
        }
    }
}


