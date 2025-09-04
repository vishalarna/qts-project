using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_DriverType_VM
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<TrainingIssue_DriverSubType_VM> SubTypes { get; set; } = new List<TrainingIssue_DriverSubType_VM>();
        public TrainingIssue_DriverType_VM() { }

        public TrainingIssue_DriverType_VM(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
