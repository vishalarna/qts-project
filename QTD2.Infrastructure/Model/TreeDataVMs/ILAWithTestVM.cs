using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TreeDataVMs
{
    public class ILAWithTestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public bool Active { get; set; }

        public List<TestTreeVM> Tests { get; set; }
    }

    public class TestTreeVM
    {
        public int Id { get; set;}

        public string TestTitle { get; set; }

        public bool Active { get; set; }
    }
}
