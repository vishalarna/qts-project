using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SaftyHazard;

namespace QTD2.Infrastructure.Model.SafetyHazard_Category
{
    public class SaftyHazardCategoryCompactOptions
    {
        public SaftyHazard_Category SaftyHazard_Category { get; set; }

        public List<SaftyHazardCompactOptions> SaftyHazardCompactOptions { get; set; } = new List<SaftyHazardCompactOptions>();
    }
}
