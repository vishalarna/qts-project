using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Locations;
using QTD2.Infrastructure.Model.Location_Category;

namespace QTD2.Infrastructure.Model.Location_Category
{
    public  class LocationCategoryCompactOptions
    {
        public QTD2.Domain.Entities.Core.Location_Category Location_Category { get; set; }

        public List<LocationCompactOptions> LocationCompactOptions { get; set; } = new List<LocationCompactOptions>();
    }
}
