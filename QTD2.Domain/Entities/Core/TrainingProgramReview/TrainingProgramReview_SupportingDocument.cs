using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgramReview_SupportingDocument : Entity
    {
        public int TrainingProgramReviewId { get; set; }
        public string Name { get; set; }
        public string Hyperlink { get; set; }
        public int Number { get; set; }

        public virtual TrainingProgramReview TrainingProgramReview { get; set; }
    }
}
