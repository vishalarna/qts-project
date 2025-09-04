using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class Version_Task : Common.Entity
    {
        public int TaskId { get; set; }

        public string TaskNumber { get; set; }
        public string VersionNumber { get; set; }

        public DateOnly? RequalificationDueDate { get;set; }
        public bool? RequalificationRequired { get;set; }
        public string? RequalificationNotes{ get;set; }
        public string Description { get; set; }

        public string Conditions { get; set; }

        public string Standards { get; set; }

        public bool Critical { get; set; }

        public string Tools { get; set; }

        public string References { get; set; }

        public int RequiredTime { get; set; }

        //public int MajorVersion { get; set; }

        //public int MinorVersion { get; set; }

        public string Abbreviation { get; set; }

        public string TaskCriteriaUpload { get; set; }

        public string Image { get; set; }

        public bool IsMeta { get; set; }

        public bool IsReliability { get; set; }

        public string Criteria { get; set; }

        public bool IsInUse { get; set; }

        // 1 for addition 0 for deletion 2 for Create
        public int State { get; set; }

        public DateOnly? EffectiveDate { get; set; }
        public bool TaskActive { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<Version_Task_Step> Version_Task_Steps { get; set; } = new List<Version_Task_Step>();

        public virtual ICollection<Version_Task_Question> Version_Task_Questions { get; set; } = new List<Version_Task_Question>();

        public virtual ICollection<Version_Task_Procedure_Link> Version_Task_Procedure_Links { get; set; } = new List<Version_Task_Procedure_Link>();

        public virtual ICollection<Version_Task_Tool_Link> Version_Task_Tool_Links { get; set; } = new List<Version_Task_Tool_Link>();

        public virtual ICollection<Version_Task_SaftyHazard_Link> Version_Task_SaftyHazard_Links { get; set; } = new List<Version_Task_SaftyHazard_Link>();

        public virtual ICollection<Version_Task_EnablingObjective_Link> Version_Task_EnablingObjective_Links { get; set; } = new List<Version_Task_EnablingObjective_Link>();
        
        public virtual ICollection<Version_Task_ILA_Link> Version_Task_ILA_Links { get; set; } = new List<Version_Task_ILA_Link>();

        public virtual ICollection<Version_Task_RR_Link> Version_Task_RR_Links { get; set; } = new List<Version_Task_RR_Link>();

        public virtual ICollection<Version_Task_TrainingGroup> Version_Task_TrainingGroups { get; set; } = new List<Version_Task_TrainingGroup>();

        public virtual ICollection<Task_History> Task_Histories { get; set; } = new List<Task_History>();

        public virtual ICollection<Version_EnablingObjective_Task> Version_EnablingObjective_Tasks { get; set; } = new List<Version_EnablingObjective_Task>();

        public virtual ICollection<Version_Task_Suggestion> Version_Task_Suggestions { get; set; } = new List<Version_Task_Suggestion>();

        public virtual ICollection<Version_Task_Position_Link> Version_Task_Position_Links { get; set; } = new List<Version_Task_Position_Link>();

        public virtual ICollection<Version_Task_MetaTask_Link> Version_Task_MetaTask_Links { get; set; } = new List<Version_Task_MetaTask_Link>();

        public Version_Task()
        {
        }

        public Version_Task(Task task, bool isUnitTesting = false, string versionNumber = "", int state = 2)
        {
            TaskId = task.Id;
            TaskNumber = Convert.ToString(task.Number);
            Description = task.Description;
            Critical = task.Critical;
            Tools = "test";
            References = task.References;
            RequiredTime = task.RequiredTime;
            VersionNumber = versionNumber;
            Abbreviation = task.Abreviation;
            IsMeta = task.IsMeta;
            IsReliability = task.IsReliability;
            TaskCriteriaUpload = task.TaskCriteriaUpload;
            Image = task.Image;
            Criteria = task.Criteria;
            IsInUse = true;
            State = state;
            this.CreatedBy = task.CreatedBy;
            this.CreatedDate = DateTime.Now;
            this.ModifiedBy = task.ModifiedBy;
            this.ModifiedDate = task.ModifiedDate;
            EffectiveDate = task.EffectiveDate;
            TaskActive = task.Active;
            if (!isUnitTesting)
            {
               // var vsh_list = task.Task_SaftyHazard_Links.Count() > 0 ? task.Task_SaftyHazard_Links?.Select(x => x.SaftyHazard?.Version_SaftyHazards).LastOrDefault().ToList() : null;

                // var vEO_list = task.Task_EnablingObjective_Links.Count() > 0 ? task.Task_EnablingObjective_Links?.OrderBy(x => x.EnablingObjective?.MajorVersion).Select(x => x.EnablingObjective?.Version_EnablingObjectives).LastOrDefault().ToList() : null;
                //var vProc_list = task.Task_Procedure_Links.Count() > 0 ? task.Task_Procedure_Links?.Select(x => x.Procedure?.Version_Procedures).LastOrDefault().ToList() : null;
                //var vTools_list = task.Task_Tools.Count() > 0 ? task.Task_Tools?.OrderBy(x => x.Tool?.MajorVersion).Select(x => x.Tool?.Version_Tools).LastOrDefault().ToList() : null;

                // var vsh_list = Version_Task_SaftyHazard_Links?.Where(x => x.Version_SaftyHazard?.MajorVersion == sH_MajorVer).Select(x => x.Version_SaftyHazard)?.ToList();
                // var vEO_list = Version_Task_EnablingObjective_Links?.Where(x => x.Version_EnablingObjective?.MajorVersion == eo_MajorVer).Select(x => x.Version_EnablingObjective)?.ToList();
                // var vProc_list = Version_Task_Procedure_Links?.Where(x => x.Version_Procedure?.MajorVersion == proc_MajorVer).Select(x => x.Version_Procedure)?.ToList();
                // var vTools_list = Version_Task_Tool_Links?.Where(x => x.Version_Tool?.MajorVersion == tool_MajorVer).Select(x => x.Version_Tool)?.ToList();
                // LinkSaftyHazards(vsh_list, vEO_list, vProc_list);
                // LinkTools(vTools_list, vEO_list, vProc_list);
                // LinkEnablingObjectives(vEO_list, vProc_list);
                // LinkProcedures(vProc_list, vEO_list);
            }
        }

        public List<Version_Task_SaftyHazard_Link> LinkSaftyHazards(
            List<Version_SaftyHazard> version_SaftyHazards,
            List<Version_EnablingObjective> version_EnablingObjectives,
            List<Version_Procedure> version_Procedures)
        {
            List<Version_Task_SaftyHazard_Link> version_Task_SaftyHazard_Links = new List<Version_Task_SaftyHazard_Link>();
            if (version_SaftyHazards != null)
            {
                foreach (var sh in version_SaftyHazards)
                {
                    string verNum;
                    if (sh.VersionNumber.Length > 0)
                    {
                        Double verNumber = Convert.ToDouble(sh.VersionNumber);
                        verNumber = verNumber + 1;
                        verNum = verNumber + ".0";
                    }
                    else
                    {
                        verNum = "1.0";
                    }
                    var vT_SH = new Version_Task_SaftyHazard_Link(Id, sh.Id, verNum);
                    version_Task_SaftyHazard_Links.Add(vT_SH);

                    if (Version_Task_SaftyHazard_Links
                        .FirstOrDefault(x => x.Version_TaskId == vT_SH.Version_TaskId && x.Version_SaftyHazardId == vT_SH.Version_SaftyHazardId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Task_SaftyHazard_Link>(vT_SH);
                    }
                }
            }

            if (version_Procedures != null)
            {
                foreach (var proc in version_Procedures)
                {
                    foreach (var sh in proc.Version_Procedure_SaftyHazard_Links)
                    {
                        string verNum;
                        if (sh.VersionNumber.Length > 0)
                        {
                            Double verNumber = Convert.ToDouble(sh.VersionNumber);
                            verNumber = verNumber + 1;
                            verNum = verNumber + ".0";
                        }
                        else
                        {
                            verNum = "1.0";
                        }
                        var vT_SH = new Version_Task_SaftyHazard_Link(Id, sh.Id, verNum);
                        version_Task_SaftyHazard_Links.Add(vT_SH);

                        if (Version_Task_SaftyHazard_Links
                            .FirstOrDefault(x => x.Version_TaskId == vT_SH.Version_TaskId
                            && x.Version_SaftyHazardId == vT_SH.Version_SaftyHazardId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_SaftyHazard_Link>(vT_SH);
                        }
                    }
                }
            }

            if (version_EnablingObjectives != null)
            {
                foreach (var eo in version_EnablingObjectives)
                {
                    foreach (var sh in eo.Version_EnablingObjective_SaftyHazard_Links)
                    {
                        string verNum;
                        if (sh.VersionNumber.Length > 0)
                        {
                            Double verNumber = Convert.ToDouble(sh.VersionNumber);
                            verNumber = verNumber + 1;
                            verNum = verNumber + ".0";
                        }
                        else
                        {
                            verNum = "1.0";
                        }
                        var vT_SH = new Version_Task_SaftyHazard_Link(Id, sh.Id, verNum);
                        version_Task_SaftyHazard_Links.Add(vT_SH);

                        if (Version_Task_SaftyHazard_Links
                            .FirstOrDefault(x => x.Version_TaskId == vT_SH.Version_TaskId
                            && x.Version_SaftyHazardId == vT_SH.Version_SaftyHazardId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_SaftyHazard_Link>(vT_SH);
                        }
                    }
                }
            }

            return version_Task_SaftyHazard_Links;
        }

        public List<Version_Task_Tool_Link> LinkTools(
            List<Version_Tool> version_Tools,
            List<Version_EnablingObjective> version_EnablingObjectives,
            List<Version_Procedure> version_Procedures)
        {
            List<Version_Task_Tool_Link> version_Task_Tool_Links = new List<Version_Task_Tool_Link>();

            if (version_Tools != null)
            {
                foreach (var tool in version_Tools)
                {
                    var vTT = new Version_Task_Tool_Link(Id, tool.Id);
                    version_Task_Tool_Links.Add(vTT);
                    if (Version_Task_Tool_Links
                        .FirstOrDefault(x => x.Version_TaskId == vTT.Version_TaskId
                        && x.Version_ToolId == vTT.Version_ToolId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Task_Tool_Link>(vTT);
                    }
                }
            }

            if (version_EnablingObjectives != null)
            {
                foreach (var vEO in version_EnablingObjectives)
                {
                    foreach (var tool in vEO.Version_EnablingObjective_Tool_Links)
                    {
                        var vTT = new Version_Task_Tool_Link(Id, tool.Id);
                        version_Task_Tool_Links.Add(vTT);
                        if (Version_Task_Tool_Links
                            .FirstOrDefault(x => x.Version_TaskId == vTT.Version_TaskId
                            && x.Version_ToolId == vTT.Version_ToolId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_Tool_Link>(vTT);
                        }
                    }
                }
            }

            if (version_Procedures != null)
            {
                foreach (var vProc in version_Procedures)
                {
                    foreach (var tool in vProc.Version_Procedure_Tool_Links)
                    {
                        var vTT = new Version_Task_Tool_Link(Id, tool.Id);
                        version_Task_Tool_Links.Add(vTT);
                        if (Version_Task_Tool_Links
                            .FirstOrDefault(x => x.Version_TaskId == vTT.Version_TaskId
                            && x.Version_ToolId == vTT.Version_ToolId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_Tool_Link>(vTT);
                        }
                    }
                }
            }

            return version_Task_Tool_Links;
        }

        public List<Version_Task_EnablingObjective_Link> LinkEnablingObjectives(List<Version_EnablingObjective> version_EnablingObjectives, List<Version_Procedure> version_Procedures)
        {
            List<Version_Task_EnablingObjective_Link> version_Task_EnablingObjective_Links = new List<Version_Task_EnablingObjective_Link>();

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
                    
                    var vT_EO = new Version_Task_EnablingObjective_Link(vEO.Id, Id, verNum);
                    version_Task_EnablingObjective_Links.Add(vT_EO);

                    if (Version_Task_EnablingObjective_Links
                        .FirstOrDefault(x => x.Version_TaskId == vT_EO.Version_TaskId
                        && x.Version_EnablingObjectiveId == vT_EO.Version_EnablingObjectiveId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Task_EnablingObjective_Link>(vT_EO);
                    }
                }
            }

            if (version_Procedures != null)
            {
                foreach (var vProc in version_Procedures)
                {
                    foreach (var vEO in vProc.Version_Procedure_EnablingObjective_Links)
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
                        var vT_EO = new Version_Task_EnablingObjective_Link(vEO.Id, Id, verNum);
                        version_Task_EnablingObjective_Links.Add(vT_EO);

                        if (Version_Task_EnablingObjective_Links
                            .FirstOrDefault(x => x.Version_TaskId == vT_EO.Version_TaskId
                        && x.Version_EnablingObjectiveId == vT_EO.Version_EnablingObjectiveId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_EnablingObjective_Link>(vT_EO);
                        }
                    }
                }
            }

            return version_Task_EnablingObjective_Links;
        }

        public List<Version_Task_Procedure_Link> LinkProcedures(List<Version_Procedure> version_Procedures, List<Version_EnablingObjective> version_EnablingObjectives)
        {
            List<Version_Task_Procedure_Link> version_Task_Procedure_Link = new List<Version_Task_Procedure_Link>();
            if (version_EnablingObjectives != null)
            {
                foreach (var vEO in version_EnablingObjectives)
                {
                    foreach (var vProc in vEO.Version_EnablingObjective_Procedure_Links)
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
                        var vT_Proc = new Version_Task_Procedure_Link(Id, vProc.Id, verNum);
                        version_Task_Procedure_Link.Add(vT_Proc);

                        if (Version_Task_Procedure_Links
                            .FirstOrDefault(x => x.Version_TaskId == vT_Proc.Version_TaskId
                            && x.Version_ProcedureId == vT_Proc.Version_ProcedureId) == null)
                        {
                            AddEntityToNavigationProperty<Version_Task_Procedure_Link>(vT_Proc);
                        }
                    }
                }
            }

            if (version_Procedures != null)
            {
                foreach (var vProc in version_Procedures)
                {
                    string verNum;
                    if (vProc.VersionNumber.Length > 0)
                    {
                        Double verNumber = Convert.ToDouble(vProc.VersionNumber);
                        verNumber = verNumber + 1;
                        verNum = verNumber + ".0";
                    }
                    else
                    {
                        verNum = "1.0";
                    }
                    var vT_Proc = new Version_Task_Procedure_Link(Id, vProc.Id, verNum);
                    version_Task_Procedure_Link.Add(vT_Proc);

                    if (Version_Task_Procedure_Links.FirstOrDefault(x => x.Version_TaskId == vT_Proc.Version_TaskId
                    && x.Version_ProcedureId == vT_Proc.Version_ProcedureId) == null)
                    {
                        AddEntityToNavigationProperty<Version_Task_Procedure_Link>(vT_Proc);
                    }
                }
            }

            return version_Task_Procedure_Link;
        }
    }
}
