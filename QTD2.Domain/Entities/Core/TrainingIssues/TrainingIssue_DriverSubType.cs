using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingIssue_DriverSubType : Common.Entity
    {
        public string SubType { get; set; }
        public int DriverTypeId { get; set; }
        public TrainingIssue_DriverType DriverType { get; set; }
        public TrainingIssue_DriverSubType() { }
        public TrainingIssue_DriverSubType(string subType, int driverTypeId)
        {
            SubType = subType;
            DriverTypeId = driverTypeId;
        }
    }
}
