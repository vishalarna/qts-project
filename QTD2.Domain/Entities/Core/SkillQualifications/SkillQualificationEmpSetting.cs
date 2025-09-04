using System;

namespace QTD2.Domain.Entities.Core
{
    public class SkillQualificationEmpSetting : Common.Entity
    {
        public int SkillQualificationId { get; set; }

        public bool ReleaseToAllSingleSignOff { get; set; }

        public int? MultipleSignOff { get; set; }

        public bool ReleaseOnReleaseDate { get; set; }

        public bool ReleaseInSpecificOrder { get; set; }

        public bool ShowSkillSuggestions { get; set; }

        public bool ShowSkillQuestions { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual SkillQualification SkillQualification { get; set; }

        public int? MultipleSignOffDisplay
        {
            get
            {
                return MultipleSignOff;
            }
        }

        public SkillQualificationEmpSetting(int skillQualificationId,
            int? multipleSignOff,
            bool releaseToAllSingleSignOff,
            bool releaseOnReleaseDate,
            bool releaseInSpecificOrder,
            bool showSkillSuggestions,
            bool showSkillQuestions
            )
        {
            SkillQualificationId = skillQualificationId;
            MultipleSignOff = multipleSignOff;
            ReleaseToAllSingleSignOff = releaseToAllSingleSignOff;
            ReleaseOnReleaseDate = releaseOnReleaseDate;
            ReleaseInSpecificOrder = releaseInSpecificOrder;
            ShowSkillSuggestions = showSkillSuggestions;
            ShowSkillQuestions = showSkillQuestions;
        }

        public SkillQualificationEmpSetting()
        {
        }
    }
}
