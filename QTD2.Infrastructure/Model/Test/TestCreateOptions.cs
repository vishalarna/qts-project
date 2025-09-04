using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test
{
    public class TestCreateOptions
    {
        public int? TestStatusId { get; set; }

        public string TestTitle { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string? Mode { get; set; }
        public bool RandomizeDistractors { get; set; } = false;
        public bool RandomizeQuestionsSequence { get; set; } = false;
    }
}
