using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class AssessmentToolTestData
    {
        public static List<AssessmentTool> Getall()
        {
            return new List<AssessmentTool>
            {
                asTool01(), asTool02()
            };
        }

        static AssessmentTool asTool01()
        {
            AssessmentTool tool = new AssessmentTool("first assessment tool");
            tool.Set_Id(1);
            return tool;
        } 

        static AssessmentTool asTool02()
        {
            AssessmentTool tool = new AssessmentTool("Second Assessment tool");
            tool.Set_Id(1);
            return tool;
        }
    }
}
