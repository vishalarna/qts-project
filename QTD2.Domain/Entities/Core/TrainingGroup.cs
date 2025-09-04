using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingGroup : Common.Entity
    {
        public int CategoryId { get; set; }

        public int GroupNumber { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public string HyperLink { get; set; }

        public byte[] PDF { get; set; }

        public virtual TrainingGroup_Category TrainingGroup_Category { get; set; }

        public virtual ICollection<Task_TrainingGroup> Task_TrainingGroups { get; set; } = new List<Task_TrainingGroup>();

        public virtual ICollection<Version_TrainingGroup> Version_TrainingGroups { get; set; } = new List<Version_TrainingGroup>();

        public TrainingGroup()
        {
        }

        public TrainingGroup(int categoryId, int groupNumber, string groupName, string groupDescription, byte[] pDF, string hyperLink)
        {
            CategoryId = categoryId;
            GroupNumber = groupNumber;
            GroupName = groupName;
            GroupDescription = groupDescription;
            PDF = pDF;
            HyperLink = hyperLink;
        }

        public Version_TrainingGroup CreateSnapshot()
        {
            return new Version_TrainingGroup(this);
        }
    }
}
