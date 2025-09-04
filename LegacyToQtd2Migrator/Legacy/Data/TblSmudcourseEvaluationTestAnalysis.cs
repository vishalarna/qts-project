using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblSmudcourseEvaluationTestAnalysis
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public int? TestItemId { get; set; }
        public bool? PassItemdifficulty { get; set; }
        public bool? PassItemdiscrimination { get; set; }
        public bool? Passitemdistractors { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TblPerspectiveCourse Course { get; set; }
        public virtual TblTestItem TestItem { get; set; }
    }
}
