using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class CustomEnablingObjective : Entity
    {
        public int? EO_TopicId { get; set; }

        public int? EO_CatId { get; set; }

        public int? EO_SubCatId { get; set; }

        public string Description { get; set; }

        public bool IsAddtoEO { get; set; }

        public int CustomEONumber { get; set; }

        public string FullNumber
        {
            get
            {
                //todo ensure fully loading and not  unloaded nav propers
                return getFullNumber();
            }
        }

        public virtual EnablingObjective_Topic EnablingObjective_Topic { get; set; }

        public virtual EnablingObjective_Category EnablingObjective_Category { get; set; }

        public virtual EnablingObjective_SubCategory EnablingObjective_SubCategory { get; set; }

        public virtual ICollection<ILACustomObjective_Link> ILACustomObjective_Links { get; set; } = new List<ILACustomObjective_Link>();

        public virtual ICollection<SegmentObjective_Link> SegmentObjective_Links { get; set; } = new List<SegmentObjective_Link>();

        public string getFullNumber()
        {
            if(EnablingObjective_Category == null)
            {
                return CustomEONumber.ToString();
            }
            else
            {
                return EnablingObjective_Category.Number
                       + "." +
                       (EnablingObjective_SubCategory == null ? "0" : EnablingObjective_SubCategory.Number)
                       + "." +
                       (EnablingObjective_Topic == null ? "0" : EnablingObjective_Topic.Number) + "." + CustomEONumber;
            }
        }

        public CustomEnablingObjective(int? eO_TopicId, string description, bool isAddtoEO = false, int? eO_CatId = null, int? eO_SubCatId = null)
        {
            EO_TopicId = eO_TopicId;
            Description = description;
            IsAddtoEO = isAddtoEO;
            EO_CatId = eO_CatId;
            EO_SubCatId = eO_SubCatId;
        }

        public CustomEnablingObjective()
        {
        }
    }
}
