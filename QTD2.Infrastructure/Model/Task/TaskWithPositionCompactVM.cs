using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskWithPositionCompactVM
    {
        public int Id { get; set; }
        public string TaskNumber { get; set; }
        public string Description { get; set; }
        public bool IsReliability{ get; set; }
        public List<string> PositionIds{ get; set; }

        public TaskWithPositionCompactVM(int id,string taskNumber, string description,bool isReliability, List<string> positionIds)
        {
            Id = id;
            TaskNumber = taskNumber;
            Description = description;
            IsReliability = isReliability;
            PositionIds = positionIds;
        }
    }
}
