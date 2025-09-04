using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudTrainingTopic
    {
        public int Ttid { get; set; }
        public long Tthid { get; set; }
        public string TrainingTopic { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }

        public virtual TblSmudTrainingTopicsHeading Tth { get; set; }
    }
}
