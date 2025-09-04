using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public partial class InstructorWorkbook_ILADesign_Resources : Common.Entity
    {
        public int ILAId { get; set; }
        public string ResourceName { get; set; }
        public string ResourcePath { get; set; }
        public int? ILA_UploadId { get; set; }
        public virtual ILA_Upload ILA_Upload { get; set; }
        public virtual InstructorWorkbook_ProspectiveILA InstructorWorkbook_ProspectiveILA { get; set; }
    }
}
