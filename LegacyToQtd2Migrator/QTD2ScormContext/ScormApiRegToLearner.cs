using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormApiRegToLearner
    {
        public ScormApiRegToLearner()
        {
            ScormRegistrations = new HashSet<ScormRegistration>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string ApiRegistrationId { get; set; }
        public string ApiLearnerId { get; set; }

        public virtual ScormApiLearner ScormApiLearner { get; set; }
        public virtual ICollection<ScormRegistration> ScormRegistrations { get; set; }
    }
}
