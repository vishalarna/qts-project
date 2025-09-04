using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ValidateCSV_Results_VM
    {
        public List<ValidationError_VM> FileValidationErrors { get; set; }
    }
}
