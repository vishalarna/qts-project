using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Locations
{
    public class LocationStatsVM
    {
        public int LocCategoryActive { get; set; }

        public int LocCategoryInactive { get; set; }

        public int LocationActive { get; set; }

        public int LocationInactive { get; set; }
    }
}
