using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_PerformTraineeEval : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int ILAId { get; set; }

        public virtual ILA ILA { get; set; }

        public ILA_PerformTraineeEval()
        {
            
        }

        public ILA_PerformTraineeEval(string title, string description, int iLAId)
        {
            Title = title;
            Description = description;
            ILAId = iLAId;
        }
    }
}
