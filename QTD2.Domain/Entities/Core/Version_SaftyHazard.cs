using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Version_SaftyHazard : Common.Entity
    {
        public int SaftyHazardId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PersonalProtectiveEquipment { get; set; }

        public int MinorVersion { get; set; }

        public int MajorVersion { get; set; }

        public string VersionNumber { get; set; }

        public virtual SaftyHazard SaftyHazard { get; set; }

        public virtual ICollection<Version_SaftyHazard_Abatement> Version_SaftyHazard_Abatements { get; set; } = new List<Version_SaftyHazard_Abatement>();

        public virtual ICollection<Version_SaftyHazard_Control> Version_SaftyHazard_Controls { get; set; } = new List<Version_SaftyHazard_Control>();

        public virtual ICollection<Version_Task_SaftyHazard_Link> Version_Task_SaftyHazard_Links { get; set; } = new List<Version_Task_SaftyHazard_Link>();

        public virtual ICollection<Version_Procedure_SaftyHazard_Link> Version_Procedure_SaftyHazard_Links { get; set; } = new List<Version_Procedure_SaftyHazard_Link>();

        public virtual ICollection<Version_EnablingObjective_SaftyHazard_Link> Version_EnablingObjective_SaftyHazard_Links { get; set; } = new List<Version_EnablingObjective_SaftyHazard_Link>();

        public Version_SaftyHazard()
        {
        }

        public Version_SaftyHazard(SaftyHazard saftyHazard, string versionNumber = null)
        {
            SaftyHazardId = saftyHazard.Id;
            Title = saftyHazard.Title;
            Description = "test";
            PersonalProtectiveEquipment = "test";
            MinorVersion = 0;
            MajorVersion = 0;
            VersionNumber = versionNumber;
        }
    }
}
