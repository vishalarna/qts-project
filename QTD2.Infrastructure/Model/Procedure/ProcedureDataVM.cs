using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure
{
    public class ProcedureDataVM
    {
        public string Number { get; set; }
        public string Title { get; set; }

        public ProcedureDataVM(string number,string title)
        {
            Number = number;
            Title = number;
        }
    }
}
