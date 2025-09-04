using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluationForm
{
    public class StudentEvaluationFormUpdateOptions
    {
        public string Name { get; set; }

        public int RatingScaleId { get; set; }

        public bool IsShared { get; set; }

        public bool IsAvailableForAllILAs { get; set; }

        public bool IsNAOption { get; set; }

        public bool IncludeComments { get; set; }
    }
}
