using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Procedure : Common.Entity
    {
        public int ProcedureId { get; set; }

        public string ProcedureNumber { get; set; }

        public string Title { get; set; }

        public int MajorVersion { get; set; }

        public int MinorVersion { get; set; }

        public string VersionNumber { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<Version_Task_Procedure_Link> Version_Task_Procedure_Links { get; set; } = new List<Version_Task_Procedure_Link>();

        public virtual ICollection<Version_Procedure_Tool_Link> Version_Procedure_Tool_Links { get; set; } = new List<Version_Procedure_Tool_Link>();

        public virtual ICollection<Version_Procedure_SaftyHazard_Link> Version_Procedure_SaftyHazard_Links { get; set; } = new List<Version_Procedure_SaftyHazard_Link>();

        public virtual ICollection<Version_EnablingObjective_Procedure_Link> Version_EnablingObjective_Procedure_Links { get; set; } = new List<Version_EnablingObjective_Procedure_Link>();

        public virtual ICollection<Version_Procedure_EnablingObjective_Link> Version_Procedure_EnablingObjective_Links { get; set; } = new List<Version_Procedure_EnablingObjective_Link>();

        public Version_Procedure()
        {
        }

        public Version_Procedure(Procedure procedure, bool isUnitTesting = false, string versionNumber = null)
        {
            ProcedureNumber = Convert.ToString(procedure.Number);
            ProcedureId = procedure.Id;
            Title = procedure.Title;
            VersionNumber = versionNumber;
            // MajorVersion = procedure.MajorVersion;
            // MinorVersion = procedure.MinorVersion;
            if (!isUnitTesting)
            {
               // var vSh = procedure.Procedure_SaftyHazard_Links.Count() > 0 ? procedure.Procedure_SaftyHazard_Links?.Select(x => x.SaftyHazard?.Version_SaftyHazards).LastOrDefault().ToList() : null;

                // var vEo = procedure.EnablingObjective_Procedure_Links.Count() > 0 ? procedure.EnablingObjective_Procedure_Links?.OrderBy(x => x?.EnablingObjective?.MajorVersion).Select(x => x?.EnablingObjective?.Version_EnablingObjectives).LastOrDefault().ToList() : null;

                // var vSh = Version_Procedure_SaftyHazard_Links?.Where(x => x.Version_SaftyHazard.MajorVersion == vsh_MajorVer).Select(x => x.Version_SaftyHazard).ToList();
                // var vEo = Version_Procedure_EnablingObjective_Links?.Where(x => x.Version_EnablingObjective.MajorVersion == vEo_MajorVer).Select(x => x.Version_EnablingObjective).ToList();
                //LinkSaftyHazards(vSh);

                // LinkEnablingObjectives(vEo);
            }
        }

        public List<Version_Procedure_SaftyHazard_Link> LinkSaftyHazards(List<Version_SaftyHazard> version_SaftyHazards)
        {
            List<Version_Procedure_SaftyHazard_Link> version_Procedure_SaftyHazard_Links = new List<Version_Procedure_SaftyHazard_Link>();
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
                    var vsh = new Version_Procedure_SaftyHazard_Link(vSh.Id, Id, verNum);
                    version_Procedure_SaftyHazard_Links.Add(vsh);

                    if (Version_Procedure_SaftyHazard_Links
                        .FirstOrDefault(x => x.Version_ProcedureId == vsh.Version_ProcedureId &&
                        x.Version_SaftyHazardId == vsh.Version_SaftyHazardId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Procedure_SaftyHazard_Link>(new Version_Procedure_SaftyHazard_Link(vSh.Id, Id, verNum));
                    }
                }
            }

            return version_Procedure_SaftyHazard_Links;
        }

        public List<Version_Procedure_EnablingObjective_Link> LinkEnablingObjectives(List<Version_EnablingObjective> version_EnablingObjectives)
        {
            List<Version_Procedure_EnablingObjective_Link> version_Procedure_EnablingObjective_Links = new List<Version_Procedure_EnablingObjective_Link>();
            if (version_EnablingObjectives != null)
            {
                foreach (var vEO in version_EnablingObjectives)
                {
                    string verNum;
                    if (vEO.VersionNumber.Length > 0)
                    {
                        Double verNumber = Convert.ToDouble(vEO.VersionNumber);
                        verNumber = verNumber + 1;
                        verNum = verNumber + ".0";
                    }
                    else
                    {
                        verNum = "1.0";
                    }
                    var veo = new Version_Procedure_EnablingObjective_Link(vEO.Id, Id, verNum);
                    version_Procedure_EnablingObjective_Links.Add(new Version_Procedure_EnablingObjective_Link(vEO.Id, Id, verNum));

                    if (Version_Procedure_EnablingObjective_Links
                        .FirstOrDefault(x => x.Version_ProcedureId == veo.Version_ProcedureId &&
                        x.Version_EnablingObjectiveId == veo.Version_EnablingObjectiveId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Procedure_EnablingObjective_Link>(veo);
                    }
                }
            }

            return version_Procedure_EnablingObjective_Links;
        }
    }
}
