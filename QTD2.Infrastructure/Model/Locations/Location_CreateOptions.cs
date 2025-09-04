using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QTD2.Infrastructure.Model.Locations
{
    public class Location_CreateOptions
    {
        public int LocCategoryID { get; set; }
        public string LocNumber { get; set; }
        public string LocName { get; set; }
        public string LocDescription { get; set; }
        public string LocAddress { get; set; }
        public string LocCity { get; set; }
        public string LocState { get; set; }
        public string LocZipCode { get; set; }
        public string LocPhone { get; set; }

        public string Notes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
