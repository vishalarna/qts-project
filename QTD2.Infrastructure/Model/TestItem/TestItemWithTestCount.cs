using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class TestItemWithTestCount
    {
        public TestItemWithTestCount(int id, string description, bool active, int count, string number)
        {
            Id = id;
            Description = description;
            Active = active;
            Count = count;
            Number = number;
        }

        public TestItemWithTestCount()
        {
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public int Count { get; set; }

        public string Number { get; set; }
    }
}
