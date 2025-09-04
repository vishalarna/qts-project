using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task_Requalification
{
    public class TQEvaluatorWithCountVM
    {
        public int EvaluatorId { get; set; }

        public string EvaluatorFName { get; set; }

        public string EvaluatorLName { get; set; }

        public int Count { get; set; }

        public string PositionTitle { get; set; }

        public List<Domain.Entities.Core.Position> Positions { get; set; } = new List<Domain.Entities.Core.Position>();


    }
}
