using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ImportData_ILA_Results_VM : ImportData_Results_VM
    {
        public List<ImportDatum_ILA_VM> Data { get; set; }
    }
}
