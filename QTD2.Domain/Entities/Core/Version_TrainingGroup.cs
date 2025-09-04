using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_TrainingGroup : Common.Entity
    {
        public int Version_TrainingGroupId { get; set; }

        public int CategoryId { get; set; }

        public int GroupNumber { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public string HyperLink { get; set; }

        public byte[] PDF { get; set; }

        public virtual TrainingGroup TrainingGroup { get; set; }

        public virtual ICollection<Version_Task_TrainingGroup> Version_Task_TrainingGroups { get; set; } = new List<Version_Task_TrainingGroup>();

        public Version_TrainingGroup()
        {
        }

        public Version_TrainingGroup(TrainingGroup tg)
        {
            Version_TrainingGroupId = tg.Id;
            CategoryId = tg.CategoryId;
            GroupNumber = tg.GroupNumber;
            GroupName = tg.GroupName;
            GroupDescription = tg.GroupDescription;
            HyperLink = tg.HyperLink;
            PDF = tg.PDF;
        }
    }
}
