using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SaftyHazard
{
    public class SaftyHazardCompactOptions
    {
        public SaftyHazardCompactOptions(int id, int saftyHazardCategoryId, string title, bool active, string number)
        {
            Id = id;
            SaftyHazardCategoryId = saftyHazardCategoryId;
            Title = title;
            Active = active;
            Number = number;
        }

        public SaftyHazardCompactOptions(int id, int saftyHazardCategoryId, string title, bool active, string number, List<int> safetyHazardSetIds)
        {
            Id = id;
            SaftyHazardCategoryId = saftyHazardCategoryId;
            Title = title;
            Active = active;
            Number = number;
            SafetyHazardSetIds = safetyHazardSetIds;
        }

        public int Id { get; set; }

        public int SaftyHazardCategoryId { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public string Number { get; set; }

        public List<int> SafetyHazardSetIds { get; set; } = new List<int>();
    }
}
