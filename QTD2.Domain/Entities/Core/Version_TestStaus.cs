using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_TestStaus : Entity
    {
        public int TestStatusId { get; set; }

        public string Description { get; set; }

        public string Version_Number { get; set; }

        public virtual TestStatus TestStatus { get; set; }

        public Version_TestStaus()
        {
        }

        public Version_TestStaus(TestStatus testStatus, string version_number = "")
        {
            TestStatusId = testStatus.Id;
            Description = testStatus.Description;
            Version_Number = version_number;
        }
    }
}
