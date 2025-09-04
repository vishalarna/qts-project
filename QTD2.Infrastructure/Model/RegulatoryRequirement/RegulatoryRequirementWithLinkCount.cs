using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RegulatoryRequirement
{
    public class RegulatoryRequirementWithLinkCount
    {
        public string Number { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public int LinkCount { get; set; }

        public bool Active { get; set; }

        public RegulatoryRequirementWithLinkCount()
        {
        }

        public RegulatoryRequirementWithLinkCount(string number, string description, int id, int linkCount, bool active)
        {
            Number = number;
            Description = description;
            Id = id;
            LinkCount = linkCount;
            Active = active;
        }
    }
}
