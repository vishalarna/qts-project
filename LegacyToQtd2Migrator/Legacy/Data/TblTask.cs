using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTask
    {
        public TblTask()
        {
            RsTblCoursesTasks = new HashSet<RsTblCoursesTask>();
            RsTblEmployeesTasks = new HashSet<RsTblEmployeesTask>();
            RsTblTasksSkillsKnowledges = new HashSet<RsTblTasksSkillsKnowledge>();
            RstblPositionsTasks = new HashSet<RstblPositionsTask>();
            TblDifsurveyEmployees = new HashSet<TblDifsurveyEmployee>();
            TblGapRatings = new HashSet<TblGapRating>();
            TblOjtevaluators = new HashSet<TblOjtevaluator>();
            TblOjthistorySteps = new HashSet<TblOjthistoryStep>();
            TblSafetyHazardTasks = new HashSet<TblSafetyHazardTask>();
            TblSmudcourseDesignTasks = new HashSet<TblSmudcourseDesignTask>();
            TblTaskSubSteps = new HashSet<TblTaskSubStep>();
            TblTasksIntroductions = new HashSet<TblTasksIntroduction>();
            TblTrainingModuleTasks = new HashSet<TblTrainingModuleTask>();
        }

        public int Tid { get; set; }
        public int? Daid { get; set; }
        public int? Tnum { get; set; }
        public int? TsubNum { get; set; }
        public string Tabbrev { get; set; }
        public string Tdesc { get; set; }
        public string Tconditions { get; set; }
        public string Tstandards { get; set; }
        public bool Critical { get; set; }
        public string Ttools { get; set; }
        public string Treferences { get; set; }
        public byte[] Ts { get; set; }
        public bool? Inactive { get; set; }

        public virtual TblDutyArea Da { get; set; }
        public virtual ICollection<RsTblCoursesTask> RsTblCoursesTasks { get; set; }
        public virtual ICollection<RsTblEmployeesTask> RsTblEmployeesTasks { get; set; }
        public virtual ICollection<RsTblTasksSkillsKnowledge> RsTblTasksSkillsKnowledges { get; set; }
        public virtual ICollection<RstblPositionsTask> RstblPositionsTasks { get; set; }
        public virtual ICollection<TblDifsurveyEmployee> TblDifsurveyEmployees { get; set; }
        public virtual ICollection<TblGapRating> TblGapRatings { get; set; }
        public virtual ICollection<TblOjtevaluator> TblOjtevaluators { get; set; }
        public virtual ICollection<TblOjthistoryStep> TblOjthistorySteps { get; set; }
        public virtual ICollection<TblSafetyHazardTask> TblSafetyHazardTasks { get; set; }
        public virtual ICollection<TblSmudcourseDesignTask> TblSmudcourseDesignTasks { get; set; }
        public virtual ICollection<TblTaskSubStep> TblTaskSubSteps { get; set; }
        public virtual ICollection<TblTasksIntroduction> TblTasksIntroductions { get; set; }
        public virtual ICollection<TblTrainingModuleTask> TblTrainingModuleTasks { get; set; }
    }
}
