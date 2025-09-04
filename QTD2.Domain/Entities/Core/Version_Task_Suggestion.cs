using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Suggestion : Common.Entity
    {
        public int Task_SuggestionId { get; set; }

        public int Version_TaskId { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public string VersionNumber { get; set; }

        public virtual Task_Suggestion Task_Suggestion { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public Version_Task_Suggestion()
        {
        }

        public Version_Task_Suggestion(Version_Task task, Task_Suggestion suggestion, string versionNumber = "1.0")
        {
            Task_SuggestionId = suggestion.Id;
            Version_TaskId = task.Id;
            Description = suggestion.Description;
            Number = suggestion.Number;
            VersionNumber = versionNumber;
        }
    }
}
