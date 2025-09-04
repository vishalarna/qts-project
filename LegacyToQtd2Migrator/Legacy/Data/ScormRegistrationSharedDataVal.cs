using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormRegistrationSharedDataVal
    {
        public ScormRegistrationSharedDataVal()
        {
            ScormRegistrationSharedData = new HashSet<ScormRegistrationSharedDatum>();
        }

        public int ScormSharedDataValueId { get; set; }
        public string Data { get; set; }
        public int? ScormRegistrationId { get; set; }
        public string GlobalObjectiveScope { get; set; }
        public string SharedDataId { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
        public virtual ICollection<ScormRegistrationSharedDatum> ScormRegistrationSharedData { get; set; }
    }
}
