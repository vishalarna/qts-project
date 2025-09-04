using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EOStatsCount
    {
        public int ActiveEO { get; set; }

        public int InActiveEO { get; set; }

        public int Tasks { get; set; }

        public int ILAs { get; set; }

        public int RRs { get; set; }

        public int Procedures { get; set; }

        public int SafetyHazards { get; set; }

        public int TestQuestions { get; set; }

        public int Positions { get; set; }

        public int Employees { get; set; }

        public int MetaEOs { get; set; }
    }
}
