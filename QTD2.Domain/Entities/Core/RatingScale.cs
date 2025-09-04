using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class RatingScale : Entity
    {
        public string Position1Text { get; set; }

        public string Position2Text { get; set; }

        public string Position3Text { get; set; }

        public string Position4Text { get; set; }

        public string Position5Text { get; set; }

        public virtual ICollection<StudentEvaluationForm> StudentEvaluationForms { get; set; } = new List<StudentEvaluationForm>();

        public RatingScale()
        {
        }

        public RatingScale(string position1Text, string position2Text, string position3Text = null, string position4Text = null, string position5Text = null)
        {
            Position1Text = position1Text;
            Position2Text = position2Text;
            Position3Text = position3Text;
            Position4Text = position4Text;
            Position5Text = position5Text;
        }
    }
}
