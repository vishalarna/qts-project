using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Step
{
    public class Task_StepDescriptionVM
    {
        public string Description { get; set; }
        public Task_StepDescriptionVM() { }

        public Task_StepDescriptionVM(string description)
        {
            Description = description;
        }
    }
}
