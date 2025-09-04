using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EnablingObjectiveVM
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }

        public EnablingObjectiveVM(){}

        public EnablingObjectiveVM(int id, string number, string description)
        {
            Id = id;
            Number = number;
            Description = description;
        }
    }

}
