using LegacyToQtd2Migrator.Releases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Helpers
{
    public class DutyAreaParsingDTO
    {
        public string DutyAreaNumber { get; set; }
        public bool TaskNumberFound { get; set; }

        public DutyAreaParsingDTO()
        {
            TaskNumberFound = false;
            DutyAreaNumber = "99";
        }

        public DutyAreaParsingDTO(string dutyAreaNumber)
        {
            DutyAreaNumber = dutyAreaNumber;

            TaskNumberFound = true;
        }
    }

    public class TaskParsingDTO
    {
        public string DutyAreaNumber { get; set; }
        public string SubDutyAreaNumber { get; set; }
        public string TaskNumber { get; set; }
        public string TaskText { get; set; }

        public bool TaskNumberFound { get; set; }

        public decimal VisionId { get; set; }

        public TaskParsingDTO(string taskText, decimal visionId)
        {
            TaskText = taskText;
            TaskNumberFound = false;

            DutyAreaNumber = "99";
            SubDutyAreaNumber = "1";

            VisionId = visionId;
        }

        public TaskParsingDTO(string dutyAreaNumber, string subDutyAreaNumber, string taskNumber, string taskText, decimal visionId)
        {
            DutyAreaNumber = dutyAreaNumber;
            SubDutyAreaNumber = subDutyAreaNumber;
            TaskNumber = taskNumber;
            TaskText = taskText;

            VisionId = visionId;
            TaskNumberFound = true;
        }
    }
}
