using QTD2.Infrastructure.Model.Certification;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA_Certification_Link;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule
{
    public class ILAClassDetailsVM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LocationName { get; set; }
        public string InstructorName { get; set; }
        public string SpecialInstructions { get; set; }
        public string WebinarLink { get; set; }
        public double? TotalTrainingHours { get; set; }
        public List<ProcedureDataVM> ProcedureDataVM { get; set; } = new List<ProcedureDataVM>();
        public List<TaskDataVM> TaskDataVM { get; set; } = new List<TaskDataVM>();
        public List<EnablingObjectiveDataVM> EnablingObjectiveDataVM { get; set; } = new List<EnablingObjectiveDataVM>();
        public ILACertificationDetailsVM ILACertificationDetailsVM { get; set; }
        public List<Certification_SubRequirementVM> Certification_SubRequirementVM { get; set; } = new List<Certification_SubRequirementVM>();

        public ILAClassDetailsVM(
            DateTime startDate,
            DateTime endDate,
            string locationName,
            string instructorName,
            string specialInstructions,
            string webinarLink,
            List<ProcedureDataVM> procedureDataVM = null,
            List<TaskDataVM> taskDataVM = null,
            List<EnablingObjectiveDataVM> enablingObjectiveDataVM = null, ILACertificationDetailsVM iLACertificationDetailsVM = null, List<Certification_SubRequirementVM> certification_SubRequirementVM = null, double? totalTrainingHours = null)
        {
            StartDate = startDate;
            EndDate = endDate;
            LocationName = locationName;
            InstructorName = instructorName;
            SpecialInstructions = specialInstructions;
            WebinarLink = webinarLink;
            ProcedureDataVM = procedureDataVM ?? new List<ProcedureDataVM>();
            TaskDataVM = taskDataVM ?? new List<TaskDataVM>();
            EnablingObjectiveDataVM = enablingObjectiveDataVM ?? new List<EnablingObjectiveDataVM>();
            ILACertificationDetailsVM = iLACertificationDetailsVM;
            Certification_SubRequirementVM = certification_SubRequirementVM ?? new List<Certification_SubRequirementVM>();
            TotalTrainingHours = totalTrainingHours;
        }
    }

}
