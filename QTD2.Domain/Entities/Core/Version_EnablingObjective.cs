using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Version_EnablingObjective : Common.Entity
    {
        public int EnablingObjectiveId { get; set; }

        public string VersionNumber { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int? TopicId { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public bool isMetaEO { get; set; }

        // 0 for Removal, 1 for Addition, 2 for updation
        public int State { get; set; }

        public bool IsInUse { get; set; }

        public virtual EnablingObjective EnablingObjective { get; set; }

        public virtual ICollection<Version_EnablingObjective_Tool_Link> Version_EnablingObjective_Tool_Links { get; set; } = new List<Version_EnablingObjective_Tool_Link>();

        public virtual ICollection<Version_Task_EnablingObjective_Link> Version_Task_EnablingObjective_Links { get; set; } = new List<Version_Task_EnablingObjective_Link>();

        public virtual ICollection<Version_EnablingObjective_SaftyHazard_Link> Version_EnablingObjective_SaftyHazard_Links { get; set; } = new List<Version_EnablingObjective_SaftyHazard_Link>();

        public virtual ICollection<Version_EnablingObjective_Procedure_Link> Version_EnablingObjective_Procedure_Links { get; set; } = new List<Version_EnablingObjective_Procedure_Link>();

        public virtual ICollection<Version_Procedure_EnablingObjective_Link> Version_Procedure_EnablingObjective_Links { get; set; } = new List<Version_Procedure_EnablingObjective_Link>();

        public virtual ICollection<Version_EnablingObjective_Task> Version_EnablingObjective_Tasks { get; set; } = new List<Version_EnablingObjective_Task>();

        public virtual ICollection<Version_EnablingObjective_ILALink> Version_EnablingObjective_ILALinks { get; set; } = new List<Version_EnablingObjective_ILALink>();
        
        public virtual ICollection<Version_EnablingObjective_RRLink> Version_EnablingObjective_RRLinks { get; set; } = new List<Version_EnablingObjective_RRLink>();

        public virtual ICollection<Version_EnablingObjective_MetaEOLink> Version_EnablingObjective_MetaEOLinks { get; set; } = new List<Version_EnablingObjective_MetaEOLink>();
        
        public virtual ICollection<Version_EnablingObjective_Position_Link> Version_EnablingObjective_Position_Links { get; set; } = new List<Version_EnablingObjective_Position_Link>();
        
        public virtual ICollection<Version_EnablingObjective_Employee_Link> Version_EnablingObjective_Employee_Links { get; set; } = new List<Version_EnablingObjective_Employee_Link>();

        public virtual ICollection<Version_TestItems> Version_TestItems { get; set; } = new List<Version_TestItems>();

        public virtual ICollection<Version_EnablingObjective_Question> Version_EnablingObjective_Questions { get; set; } = new List<Version_EnablingObjective_Question>();

        public virtual ICollection<Version_EnablingObjective_Step> Version_EnablingObjective_Steps { get; set; } = new List<Version_EnablingObjective_Step>();

        public virtual ICollection<EnablingObjectiveHistory> EnablingObjectiveHistories { get; set; } = new List<EnablingObjectiveHistory>();

        public virtual ICollection<Version_EnablingObjective_Suggestions> Version_EnablingObjective_Suggestions { get; set; } = new List<Version_EnablingObjective_Suggestions>();
        
        public Version_EnablingObjective()
        {
        }

        //public Version_EnablingObjective(EnablingObjective enablingObjective, bool isUnitTesting = false, string versionNumber = "")
        //{
        //    EnablingObjectiveId = enablingObjective.Id;
        //    EnablingObjectiveNumber = Convert.ToString(enablingObjective.Number);
        //    Description = enablingObjective.Description;
        //    if (!isUnitTesting)
        //    {
        //        var vSh = enablingObjective.EnablingObjective_SaftyHazard_Links.Count() > 0 ? enablingObjective?.EnablingObjective_SaftyHazard_Links?.Select(x => x?.SaftyHazard?.Version_SaftyHazards).LastOrDefault().ToList() : null;
        //        var vProc = enablingObjective.Procedure_EnablingObjective_Links.Count() > 0 ? enablingObjective?.Procedure_EnablingObjective_Links?.Select(x => x?.Procedure?.Version_Procedures).LastOrDefault().ToList() : null;

        //        // var vSh = Version_EnablingObjective_SaftyHazard_Links?.Where(x => x?.Version_SaftyHazard?.MajorVersion == vsh_MajorVer).Select(x => x?.Version_SaftyHazard).ToList();
        //        // var vProc = Version_EnablingObjective_Procedure_Links?.Where(x => x?.Version_Procedure?.MajorVersion == vProc_MajorVer).Select(x => x?.Version_Procedure).ToList();
        //        LinkSaftyHazards(vSh);
        //        LinkProcedures(vProc);
        //    }
        //}

        public Version_EnablingObjective(EnablingObjective enablingObjective, string versionNumber = "", int state = 0)
        {
            EnablingObjectiveId = enablingObjective.Id;
            VersionNumber = versionNumber;
            CategoryId = enablingObjective.CategoryId;
            SubCategoryId = enablingObjective.SubCategoryId;
            TopicId = enablingObjective.TopicId;
            Number = enablingObjective.Number;
            Description = enablingObjective.Description;
            this.isMetaEO = isMetaEO;
            this.State = state;
        }

        public List<Version_EnablingObjective_SaftyHazard_Link> LinkSaftyHazards(List<Version_SaftyHazard> version_SaftyHazards)
        {
            List<Version_EnablingObjective_SaftyHazard_Link> version_EnablingObjective_SaftyHazard_Links = new List<Version_EnablingObjective_SaftyHazard_Link>();
            if (version_SaftyHazards != null)
            {
                foreach (var vSh in version_SaftyHazards)
                {
                    string verNum;
                    if (vSh.VersionNumber.Length > 0)
                    {
                        Double verNumber = Convert.ToDouble(vSh.VersionNumber);
                        verNumber = verNumber + 1;
                        verNum = verNumber + ".0";
                    }
                    else
                    {
                        verNum = "1.0";
                    }
                    var vEO_SH = new Version_EnablingObjective_SaftyHazard_Link(Id, vSh.Id, verNum);
                    version_EnablingObjective_SaftyHazard_Links.Add(vEO_SH);

                    if (Version_EnablingObjective_SaftyHazard_Links
                        .FirstOrDefault(x => x.Version_EnablingObjectiveId == vEO_SH.Version_EnablingObjectiveId &&
                        x.Version_SaftyHazardId == vEO_SH.Version_SaftyHazardId) == null)
                    {
                        AddEntityToNavigationProperty<Version_EnablingObjective_SaftyHazard_Link>(vEO_SH);
                    }
                }
            }

            return version_EnablingObjective_SaftyHazard_Links;
        }

        public List<Version_EnablingObjective_Procedure_Link> LinkProcedures(List<Version_Procedure> version_Procedures)
        {
            List<Version_EnablingObjective_Procedure_Link> version_EnablingObjective_Procedure_Links = new List<Version_EnablingObjective_Procedure_Link>();
            if (version_Procedures != null)
            {
                foreach (var vProc in version_Procedures)
                {
                    var vEO_Proc = new Version_EnablingObjective_Procedure_Link(Id, vProc.Id);
                    version_EnablingObjective_Procedure_Links.Add(vEO_Proc);

                    if (Version_EnablingObjective_Procedure_Links
                        .FirstOrDefault(x => x.Version_EnablingObjectiveId == vEO_Proc.Version_EnablingObjectiveId
                        && x.Version_ProcedureId == vEO_Proc.Version_ProcedureId) == null)
                    {
                        AddEntityToNavigationProperty<Version_EnablingObjective_Procedure_Link>(vEO_Proc);
                    }
                }
            }

            return version_EnablingObjective_Procedure_Links;
        }
    }
}
