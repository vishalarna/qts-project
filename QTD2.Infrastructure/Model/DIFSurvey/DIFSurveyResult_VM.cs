using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DIFSurvey
{
    public class DIFSurveyResult_VM
    {
        public string Position { get; set; }
        public string title { get; set; }
        public string RatingScale { get; set; }
        public List<DIFTaskRating_VM> TaskRatings { get; set; }
    }
}
