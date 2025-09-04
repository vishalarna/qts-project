using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ImportDatum_Class_VM
    {
        public string ClassILANum { get; set; }
        public string StartDate { get; set; }
        public string ClassEndDate { get; set; }
        public string InstructorName { get; set; }
        public string Location { get; set; }
        public string EmpNum { get; set; }
        public string CompGrade { get; set; }
        public List<ValidationError_VM> ValidationErrors { get; set; }
    }
}
