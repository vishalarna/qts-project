using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Position : Common.Entity
    {
        public int PositionId;

        public int PositionNumber { get; set; }

        public string PositionAbbreviation { get; set; }

        public string PositionTitle { get; set; }

        public string PositionDescription { get; set; }

        public string HyperLink { get; set; }

        public bool IsPublished { get; set; }

        public byte[] PositionsFileUpload { get; set; }

        public virtual Position Position { get; set; }

        public virtual ICollection<Version_EnablingObjective_Position_Link> Version_EnablingObjective_Position_Links { get; set; } = new List<Version_EnablingObjective_Position_Link>();

        public virtual ICollection<Version_Task_Position_Link> Version_Task_Position_Links { get; set; } = new List<Version_Task_Position_Link>();

        public Version_Position()
        {
        }

        public Version_Position(Position position)
        {
            PositionId = position.Id;
            PositionNumber = position.PositionNumber;
            PositionAbbreviation = position.PositionAbbreviation;
            PositionTitle= position.PositionTitle;
            PositionDescription= position.PositionDescription;
            HyperLink = position.HyperLink;
            IsPublished= position.IsPublished;
            PositionsFileUpload = position.PositionsFileUpload;
        }
    }
}
