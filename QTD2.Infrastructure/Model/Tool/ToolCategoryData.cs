using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolCategoryData: Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public DateTime? EffectiveDate { get; set; }
        public string Notes { get; set; }

    }
}
