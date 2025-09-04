using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Tool
{
    public class ToolOptions: Entity
    {
        public int ToolCategoryId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string Hyperlink { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string Description { get; set; }
    }
}
