using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RatingScaleN : Entity
    {
        public string RatingScaleDescription { get; set; }

        public virtual ICollection<StudentEvaluation> StudentEvaluations { get; set; } = new List<StudentEvaluation>();
        public virtual ICollection<RatingScaleExpanded> RatingScaleExpanded { get; set; } = new List<RatingScaleExpanded>();

        public RatingScaleN()
        {

        }
        public RatingScaleN(string description)
        {
            RatingScaleDescription = description;
        }
    }
    
}
