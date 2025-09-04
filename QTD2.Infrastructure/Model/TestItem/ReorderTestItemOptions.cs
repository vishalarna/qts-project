using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TestItem
{
    public class ReorderTestItemOptions
    {
        public List<ItemOrder> TestItemOrder { get; set; } = new List<ItemOrder>(); 
    }

    public class ItemOrder
    {
        public int ItemId { get; set; }

        public int Order { get; set; }
    }
}
