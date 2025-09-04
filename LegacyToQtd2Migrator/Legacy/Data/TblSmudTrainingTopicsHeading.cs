using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudTrainingTopicsHeading
    {
        public TblSmudTrainingTopicsHeading()
        {
            TblSmudTrainingTopics = new HashSet<TblSmudTrainingTopic>();
        }

        public long Tthid { get; set; }
        public string TrainingTopicHeading { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }

        public virtual ICollection<TblSmudTrainingTopic> TblSmudTrainingTopics { get; set; }
    }
}
