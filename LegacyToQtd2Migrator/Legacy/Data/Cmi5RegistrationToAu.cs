using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class Cmi5RegistrationToAu
    {
        public Cmi5RegistrationToAu()
        {
            Cmi5Sessions = new HashSet<Cmi5Session>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormRegistrationId { get; set; }
        public int ScormObjectId { get; set; }
        public string LaunchParameters { get; set; }
        public string MoveOn { get; set; }
        public decimal? MasteryScore { get; set; }
        public bool IsSatisfied { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual TinCanRegistration TinCanRegistration { get; set; }
        public virtual ICollection<Cmi5Session> Cmi5Sessions { get; set; }
    }
}
