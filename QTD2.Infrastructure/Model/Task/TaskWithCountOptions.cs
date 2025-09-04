using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskWithCountOptions
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public int TrainingGroupLinkCount { get; set; }

        public bool Active { get; set; }

        public int DANumber { get; set; }

        public int SDANumber { get; set; }

        public string Letter { get; set; }

        public bool IsUsedForTQ { get; set; }

        public int TaskNumber { get; set; }

        public string OrderProperty { get; set; }

        public bool? IsRR { get; set; }

        public TaskWithCountOptions()
        {
        }

        public TaskWithCountOptions(string number, string description, int id, int linkCount, bool active, int trainingGroupLinkCount,bool isRR = false)
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
            TrainingGroupLinkCount = trainingGroupLinkCount;
            IsRR = isRR;
        }
    }
}
