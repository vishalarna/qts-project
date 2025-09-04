using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CustomEnablingObjective
{
    public class CustomEnablingObjectiveCreateOptions
    {
        public int? EO_TopicId { get; set; }

        public int? EO_CatId { get; set; }

        public int? EO_SubCatId { get; set; }

        public string Description { get; set; }

        public bool IsAddtoEO { get; set; } = false;
    }
}
