using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingTopic : Entity
    {
        public int TrainingTopic_CategoryId { get; set; }

        public string Name { get; set; }

        public virtual TrainingTopic_Category TrainingTopic_Category { get; set; }

        public virtual ICollection<ILA_TrainingTopic_Link> ILA_TrainingTopic_Links { get; set; } = new List<ILA_TrainingTopic_Link>();

        public TrainingTopic(string name, int trTopicCatId)
        {
            Name = name;
            TrainingTopic_CategoryId = trTopicCatId;
        }

        public TrainingTopic()
        {
        }
    }
}
