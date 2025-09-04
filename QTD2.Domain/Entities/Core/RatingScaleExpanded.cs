using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class RatingScaleExpanded : Entity
    {
        public virtual RatingScaleN RatingScaleNInfo { get; set; }
        public string Description { get; set; }
        public int Ratings { get; set; }
        public int RatingScaleNId { get; set; }

        public virtual List<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; }

        public RatingScaleExpanded()
        {

        }
        public RatingScaleExpanded(string Description, int RatingScaleNId,int Ratings)
        {
            this.Description = Description;
            this.RatingScaleNId= RatingScaleNId;
            this.Ratings = Ratings;
        }

    }

}
