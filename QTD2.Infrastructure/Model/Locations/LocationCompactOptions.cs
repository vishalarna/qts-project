using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Locations
{

    public class LocationCompactOptions
    {
        public LocationCompactOptions(int id,  string locNum , int locCategoryID, string name, bool active)
        {
            Id = id;
            LocNum = locNum;
            LocCategoryID = locCategoryID;
            Name = name;
            Active = active;
        }

        public int Id { get; set; }
        public string LocNum { get; set; }

        public int LocCategoryID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

}


