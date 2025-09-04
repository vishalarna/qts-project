using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSkillsKnowledge
    {
        public TblSkillsKnowledge()
        {
            RsTblCoursesSkillsKnowledges = new HashSet<RsTblCoursesSkillsKnowledge>();
            RsTblTasksSkillsKnowledges = new HashSet<RsTblTasksSkillsKnowledge>();
            TblCoSks = new HashSet<TblCoSk>();
            TblSafetyHazardEos = new HashSet<TblSafetyHazardEo>();
            TblSkProcedures = new HashSet<TblSkProcedure>();
            TblTdtrandomReviews = new HashSet<TblTdtrandomReview>();
            TblTestItems = new HashSet<TblTestItem>();
            TblTrainingModuleSks = new HashSet<TblTrainingModuleSk>();
        }

        public int Skid { get; set; }
        public int? Cid { get; set; }
        public int? Sknum { get; set; }
        public int? SksubNum { get; set; }
        public string Skdesc { get; set; }
        public byte[] Ts { get; set; }
        public bool Inactive { get; set; }

        public virtual TblCategory CidNavigation { get; set; }
        public virtual ICollection<RsTblCoursesSkillsKnowledge> RsTblCoursesSkillsKnowledges { get; set; }
        public virtual ICollection<RsTblTasksSkillsKnowledge> RsTblTasksSkillsKnowledges { get; set; }
        public virtual ICollection<TblCoSk> TblCoSks { get; set; }
        public virtual ICollection<TblSafetyHazardEo> TblSafetyHazardEos { get; set; }
        public virtual ICollection<TblSkProcedure> TblSkProcedures { get; set; }
        public virtual ICollection<TblTdtrandomReview> TblTdtrandomReviews { get; set; }
        public virtual ICollection<TblTestItem> TblTestItems { get; set; }
        public virtual ICollection<TblTrainingModuleSk> TblTrainingModuleSks { get; set; }
    }
}
