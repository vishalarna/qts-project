using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgram : Common.Entity
    {
        public int PositionId { get; set; }
        public int TrainingProgramTypeId { get; set; }

        public string TPVersionNo { get; set; }

        public string ProgramTitle { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public DateTime Year { get; set; }

        public bool? Publish { get; set; }

        public string Version
        {
            get
            {
                switch (TrainingProgramTypeId)
                {
                    case 1:
                        return $"{TPVersionNo}: {StartDate.ToShortDateString()} - {EndDate?.ToShortDateString() ?? ""}";
                    case 2:
                        return Year.Year.ToString();
                    case 3:
                        return $"{ProgramTitle}: {StartDate.ToShortDateString()} - {EndDate?.ToShortDateString() ?? ""}";
                    default:
                        return "";
                }
            }
        }

        public virtual Position Position { get; set; }
        public virtual TrainingProgramType TrainingProgramType { get; set; }
        public virtual ICollection<TrainingPrograms_ILA_Link> TrainingProgram_ILA_Links { get; set; } = new List<TrainingPrograms_ILA_Link>();
        public virtual ICollection<TrainingProgram_History> TrainingProgram_Histories { get; set; } = new List<TrainingProgram_History>();
        public virtual ICollection<TrainingProgramReview> TrainingProgramReviews { get; set; } = new List<TrainingProgramReview>();
        public virtual ICollection<TrainingProgramReview_TrainingIssue_Link> TrainingProgramReview_TrainingIssue_Links { get; set; } = new List<TrainingProgramReview_TrainingIssue_Link>();
        public TrainingProgram(
            int positionId,
            string version,
            string programTitle,
            int programType,
            DateTime startDate,
            DateTime? endDate,
            DateTime year,
            string description,
            bool? publish)
        {
            PositionId = positionId;
            TPVersionNo = version;
            ProgramTitle = programTitle;
            TrainingProgramTypeId = programType;
            StartDate = startDate;
            EndDate = endDate;
            Year = year;
            Description = description;
            Publish = publish;
        }

        public TrainingProgram()
        {
        }

        public TrainingPrograms_ILA_Link LinkILA(ILA ila)
        {
            TrainingPrograms_ILA_Link t_ila_link = TrainingProgram_ILA_Links.FirstOrDefault(x => x.TrainingProgramId == this.Id && x.ILAId == ila.Id);
            if (t_ila_link != null)
            {
                return t_ila_link;
            }

            t_ila_link = new TrainingPrograms_ILA_Link(this, ila);
            AddEntityToNavigationProperty<TrainingPrograms_ILA_Link>(t_ila_link);
            return t_ila_link;
        }

        public void UnlinkILA(ILA ila)
        {
            TrainingPrograms_ILA_Link t_ila_link = TrainingProgram_ILA_Links.FirstOrDefault(x => x.TrainingProgramId == this.Id && x.ILAId == ila.Id);
            if (t_ila_link != null)
            {
                RemoveEntityFromNavigationProperty<TrainingPrograms_ILA_Link>(t_ila_link);
            }
        }
    }
}
