using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class CbtScormUpload
    {
        public CbtScormUpload()
        {
            CbtScormRegistrations = new HashSet<CbtScormRegistration>();
        }

        public int Id { get; set; }
        public int CbtId { get; set; }
        public string Name { get; set; }
        public string ScormStatus { get; set; }
        public DateTime ConnectedDate { get; set; }
        public DateTime? DisconnectedDate { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Cbt Cbt { get; set; }
        public virtual ICollection<CbtScormRegistration> CbtScormRegistrations { get; set; }
    }
}
