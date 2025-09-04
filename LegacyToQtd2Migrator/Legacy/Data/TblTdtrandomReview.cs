using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTdtrandomReview
    {
        public int RandomReviewId { get; set; }
        public int Skid { get; set; }
        public int TestId { get; set; }
        public int AvailableItems { get; set; }
        public int NumberOfItems { get; set; }
        public bool MultipleChoice { get; set; }
        public bool TrueFalse { get; set; }
        public bool Matching { get; set; }
        public bool FillInTheBlanks { get; set; }
        public bool ShortAnswer { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblSkillsKnowledge Sk { get; set; }
        public virtual TblTest Test { get; set; }
    }
}
