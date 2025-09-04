using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskDataVM
    {
        public string FullNumber { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public TaskDataVM(string fullNumber,string description, string type)
        {
            FullNumber = fullNumber;
            Description = description;
            Type = type;
        }
    }
}
