using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SafetyHazard_Category
{
    public class SafetyHazard_CategoryCreateOptions
    {
        public string Description { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public string? Notes { get; set; }

        public DateTime? EffectiveDate { get; set; }
    }
}
