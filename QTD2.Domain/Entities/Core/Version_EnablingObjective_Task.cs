using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective_Task : Entity
    {
        public int Version_EnablingObjectiveId { get; set; }

        public int Version_TaskId { get; set; }

        public string Version_Number { get; set; }

        public virtual Version_EnablingObjective Version_EnablingObjective { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public Version_EnablingObjective_Task()
        {
        }

        public Version_EnablingObjective_Task(Version_EnablingObjective version_EnablingObjective, Version_Task version_Task, string version_number = "")
        {
            Version_EnablingObjectiveId = version_EnablingObjective.Id;
            Version_TaskId = version_Task.Id;
            Version_EnablingObjective = version_EnablingObjective;
            Version_Task = version_Task;
            Version_Number = version_number;
        }
    }
}
