using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Location_Category
{
    public  class Location_CategoryCreateOptions
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string website { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string CategoryNotes { get; set; }
    }
}
