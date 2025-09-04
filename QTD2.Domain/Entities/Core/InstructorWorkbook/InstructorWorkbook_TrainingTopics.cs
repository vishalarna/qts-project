using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
   public partial class InstructorWorkbook_TrainingTopics :Common.Entity
    {
        public int TTHID { get; set; }
        public string TrainingTopic { get; set; }

        public virtual InstructorWorkbook_TrainingTopicsHeading InstructorWorkbook_TrainingTopicsHeading { get; set; }
}
}
