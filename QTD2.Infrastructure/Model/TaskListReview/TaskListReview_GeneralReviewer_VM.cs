using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskListReview
{
    public class TaskListReview_GeneralReviewer_VM
    {
        public int QTDUserId { get; set; }
        public string Name { get; set; }

        public TaskListReview_GeneralReviewer_VM(int qtdUserId, string name)
        {
            QTDUserId = qtdUserId;
            Name = name;
        }
    }
}
