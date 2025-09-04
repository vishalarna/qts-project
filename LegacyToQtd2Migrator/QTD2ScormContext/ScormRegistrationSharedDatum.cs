using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormRegistrationSharedDatum
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormRegistrationId { get; set; }
        public int ScormRegistrationDataId { get; set; }
        public string SharedDataId { get; set; }
        public int ScormSharedDataValueId { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
        public virtual ScormRegistrationSharedDataVal ScormRegistrationSharedDataVal { get; set; }
    }
}
