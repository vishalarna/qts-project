using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Locations
{
    public class LocationLatestActivityVM
    {
        public int LocId { get; set; }
        public string LocCategoryID { get; set; }
        public string LocNumber { get; set; }
        public string LocName { get; set; }
        public string ActivityDesc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    

    }
}
