using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingIssue_DriverType : Common.Entity
    {
        public string Type { get; set; }
        public List<TrainingIssue_DriverSubType> DriverSubTypes { get; set; } = new List<TrainingIssue_DriverSubType>();
    }
}
