using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluation
{
    public class StudentEvaluationCreateOptions
    {
        public int RatingScaleId { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsAvailableForAllILAs { get; set; }
        public bool? IsAvailableForSelectedILAs { get; set; }

        public bool? IsIncludeCommentSections { get; set; }

        public bool? IsAllowNAOption { get; set; }

        public string Mode { get; set; }

        public string? AnotherMode { get; set; }

        public int? stdEvalId  { get; set; }

        public DateTime EffectiveDate { get; set; }
        public string Notes { get; set; }


    }
}
