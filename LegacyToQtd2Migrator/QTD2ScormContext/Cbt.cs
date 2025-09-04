using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class Cbt
    {
        public Cbt()
        {
            CbtScormUploads = new HashSet<CbtScormUpload>();
        }

        public int Id { get; set; }
        public int Ilaid { get; set; }
        public int Availablity { get; set; }
        public string CbtlearningContractInstructions { get; set; }
        public int DueDateAmount { get; set; }
        //public int DueDateInterval { get; set; }
        public bool? Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? EmpSettingsReleaseTypeId { get; set; }

        public virtual Ila Ila { get; set; }
        public virtual ICollection<CbtScormUpload> CbtScormUploads { get; set; }
    }
}
