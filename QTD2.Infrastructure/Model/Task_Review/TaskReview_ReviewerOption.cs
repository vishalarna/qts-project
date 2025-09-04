using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Review
{
    public class TaskReview_ReviewerOption
    {
        public int QtdUserId { get; set; }

        public TaskReview_ReviewerOption(int id)
        {
            QtdUserId = id;
        }
        public TaskReview_ReviewerOption()
        {

        }
    }
}
