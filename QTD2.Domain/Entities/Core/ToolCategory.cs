using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ToolCategory : Common.Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }
        public virtual ICollection<ToolCategory_StatusHistory> ToolCategories_StatusHistories { get; set; } = new List<ToolCategory_StatusHistory>();

        public ToolCategory(string title, string description, string website, DateTime? effectiveDate, string notes)
        {
            Title = title;
            Description = description;
            Website = website;
            EffectiveDate = effectiveDate;
            Notes = notes;
        }

        public ToolCategory()
        {
        }
    }
}
