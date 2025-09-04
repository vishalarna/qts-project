using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SafetyHazardWithLinkCount
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Number { get; set; }

        public int LinkCount { get; set; }

        public bool Active { get; set; }

        public string? Category_Title { get; set; }


        public SafetyHazardWithLinkCount()
        {
        }

        public SafetyHazardWithLinkCount(int id, string title, string number, int linkCount, bool active, string? categoryTitle = null)
        {
            Id = id;
            Title = title;
            Number = number;
            LinkCount = linkCount;
            Active = active;
            Category_Title = categoryTitle;
        }
    }
}
