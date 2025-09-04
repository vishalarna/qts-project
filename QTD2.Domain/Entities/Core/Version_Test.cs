using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Test : Entity
    {
        public int TestStatusId { get; set; }

        public string TestTitle { get; set; }

        public int TestId { get; set; }

        public string Version_Number { get; set; }

        public int State { get; set; }

        public bool IsInUse { get; set; }

        public virtual TestStatus TestStatus { get; set; }

        public virtual Test Test { get; set; }

        public Version_Test()
        {
        }

        public Version_Test(Test test, string version_number = "",int state = 0)
        {
            TestTitle = test.TestTitle;
            TestId = test.Id;
            Version_Number = version_number;
            TestStatusId = test.TestStatusId;
        }
    }
}
