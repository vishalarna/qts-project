using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanRegistration
    {
        public TinCanRegistration()
        {
            Cmi5RegistrationToAus = new HashSet<Cmi5RegistrationToAu>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormRegistrationId { get; set; }
        public byte[] TincanRegistrationId { get; set; }
        public string Completion { get; set; }
        public string Success { get; set; }
        public bool ScoreIsKnown { get; set; }
        public decimal Score { get; set; }
        public long TotalSecondsTracked { get; set; }
        public string CompOfFailedSuccess { get; set; }
        public bool? Cmi5CourseSatisfied { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
        public virtual ICollection<Cmi5RegistrationToAu> Cmi5RegistrationToAus { get; set; }
    }
}
