using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTestItem
    {
        public TblTestItem()
        {
            RsTblTestTestItems = new HashSet<RsTblTestTestItem>();
            TblSmudcourseEvaluationTestAnalyses = new HashSet<TblSmudcourseEvaluationTestAnalysis>();
            TblTdtrandomReviewDetails = new HashSet<TblTdtrandomReviewDetail>();
            TblTestitemDistractors = new HashSet<TblTestitemDistractor>();
        }

        public int TestItemId { get; set; }
        public int? Skid { get; set; }
        public string Stem { get; set; }
        public int? TestItemType { get; set; }
        public int? TaxonomyId { get; set; }
        public int? ImageId { get; set; }
        public byte[] Ts { get; set; }
        public bool? Active { get; set; }
        public int ImageSizeId { get; set; }
        public bool? Deleted { get; set; }

        public virtual TblTdtimage Image { get; set; }
        public virtual TblSkillsKnowledge Sk { get; set; }
        public virtual TblTaxonomy Taxonomy { get; set; }
        public virtual TblTestItemType TestItemTypeNavigation { get; set; }
        public virtual ICollection<RsTblTestTestItem> RsTblTestTestItems { get; set; }
        public virtual ICollection<TblSmudcourseEvaluationTestAnalysis> TblSmudcourseEvaluationTestAnalyses { get; set; }
        public virtual ICollection<TblTdtrandomReviewDetail> TblTdtrandomReviewDetails { get; set; }
        public virtual ICollection<TblTestitemDistractor> TblTestitemDistractors { get; set; }
    }
}
