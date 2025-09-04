using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_DriverSubType_VM
    {
        public int Id { get; set; }
        public string SubType { get; set; }

        public TrainingIssue_DriverSubType_VM(int id, string subType)
        {
            Id = id;
            SubType = subType;
        }

        public TrainingIssue_DriverSubType_VM()
        {
        }
    }
}
