using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormApiLearner
    {
        public ScormApiLearner()
        {
            ScormApiRegToLearners = new HashSet<ScormApiRegToLearner>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public string ApiLearnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ScormApiRegToLearner> ScormApiRegToLearners { get; set; }
    }
}
