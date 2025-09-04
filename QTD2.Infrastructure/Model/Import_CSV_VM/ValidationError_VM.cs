using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ValidationError_VM
    {
        public string Error { get; set; }

        public ValidationError_VM(string error)
        {
            Error = error;
        }

    }
}
