using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CustomEnablingObjective
{
    public class CustomEnablingObjectiveUpdateOptions
    {
        public int? EO_TopicId { get; set; }

        public string Description { get; set; }

        public bool IsAddtoEO { get; set; } = false;

        public int? Eo_CatId { get; set; }
        
        public int? Eo_SubCatId { get; set; }
    }
}
