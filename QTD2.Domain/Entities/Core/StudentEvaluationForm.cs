using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluationForm : Entity
    {
        public string Name { get; set; }

        public int RatingScaleId { get; set; }

        public bool IsShared { get; set; }

        public bool IsAvailableForAllILAs { get; set; }

        public bool IsNAOption { get; set; }

        public bool IncludeComments { get; set; }

        public virtual RatingScale RatingScale { get; set; }

        public virtual ICollection<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; } = new List<ILA_StudentEvaluation_Link>();

        public virtual ICollection<StudentEvaluationQuestion> StudentEvaluationQuestions { get; set; } = new List<StudentEvaluationQuestion>();

        public StudentEvaluationForm(string name, int ratingScaleId, bool isShared, bool isAvailableForAllILAs, bool isNAOption, bool includeComments)
        {
            Name = name;
            RatingScaleId = ratingScaleId;
            IsShared = isShared;
            IsAvailableForAllILAs = isAvailableForAllILAs;
            IsNAOption = isNAOption;
            IncludeComments = includeComments;
        }

        public StudentEvaluationForm()
        {
        }
    }
}
