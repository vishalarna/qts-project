using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_TrainingGroup : Common.Entity
    {
        public int Version_TaskId { get; set; }

        public int Version_TrainingGroupId { get; set; }

        public Version_Task Version_Task { get; set; }

        public Version_TrainingGroup Version_TrainingGroup { get; set; }

        public Version_Task_TrainingGroup()
        {
        }

        public Version_Task_TrainingGroup(int version_TaskId, int version_TrainingGroupId)
        {
            Version_TaskId = version_TaskId;
            Version_TrainingGroupId = version_TrainingGroupId;
        }
    }
}
