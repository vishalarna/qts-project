using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_TrainingTopic_Link : Entity
    {
        public int ILAId { get; set; }

        public int TrTopicId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual TrainingTopic TrainingTopic { get; set; }

        public ILA_TrainingTopic_Link(ILA iLA, TrainingTopic trTopic)
        {
            ILAId = iLA.Id;
            TrTopicId = trTopic.Id;
            ILA = iLA;
            TrainingTopic = trTopic;
        }

        public ILA_TrainingTopic_Link()
        {
        }
    }
}
