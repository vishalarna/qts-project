using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class ToolGroupTestData
    {
        public static List<ToolGroup> GetAll()
        {
            return new List<ToolGroup>()
            {
                TG1(),TG2()
            };
        }

        static ToolGroup TG1()
        {
            var tg = new ToolGroup("Cutting Tools", true);
            tg.Set_Id(1);
            return tg;
        }

        static ToolGroup TG2()
        {
            var tg = new ToolGroup("Safety Tools", true);
            tg.Set_Id(2);
            return tg;
        }
    }
}
