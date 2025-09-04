using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Instructor : Common.Entity
    {
        public int? ICategoryId { get; set; }

        public string InstructorNumber { get; set; }

        public string InstructorName { get; set; }

        public string InstructorEmail { get; set; }

        public string InstructorDescription { get; set; }

        public bool IsWorkBookAdmin { get; set; }

        public DateTime EffectiveDate { get; set; }

        public virtual Instructor_Category Instructor_Category { get; set; }

        public virtual ICollection<Instructor_History> Instructor_Histories { get; set; } = new List<Instructor_History>();

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
        public Instructor(int catid, string num, string name, string email, string description, bool iswoorkbookadmin, DateTime effectiveDate)
        {
            ICategoryId = catid;
            InstructorNumber = num;
            InstructorEmail = email;
            InstructorName = name;
            InstructorDescription = description;
            IsWorkBookAdmin = iswoorkbookadmin;
            EffectiveDate = effectiveDate;
        }
        public Instructor()
        {
        }
        public void setCatIdAndInstructorNum(int? iCategoryId, string instructorNumber)
        {
            ICategoryId = iCategoryId;
            InstructorNumber = instructorNumber;
        }
    }
}
