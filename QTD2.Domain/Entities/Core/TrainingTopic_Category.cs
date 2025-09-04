using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingTopic_Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<TrainingTopic> TrainingTopics { get; set; } = new List<TrainingTopic>();

        public TrainingTopic_Category(string name)
        {
            Name = name;
        }

        public TrainingTopic_Category()
        {
        }
    }
}
