using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TestStatus : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

        public virtual ICollection<Version_TestStaus> Version_TestStauses { get; set; } = new List<Version_TestStaus>();

        public virtual ICollection<Version_Test> Version_Tests { get; set; } = new List<Version_Test>();

        public TestStatus(string description)
        {
            Description = description;
        }

        public TestStatus()
        {

        }
    }
}
