using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolDataVM
    {
        public string Name { get; set; }

        public ToolDataVM() { }

        public ToolDataVM(string name)
        {
            Name = name;
        }
    }
}
