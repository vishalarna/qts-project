using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Instructor_Category : Common.Entity
    {
        public string ICategoryTitle { get; set; }

        public string ICategoryDescription { get; set; }

        public string ICategoryUrl { get; set; }
        public DateTime? IEffectiveDate { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        public virtual ICollection<Instructor_CategoryHistory> Instructor_CategoryHistories { get; set; } = new List<Instructor_CategoryHistory>();

        public Instructor_Category(string title, string description, string website, DateTime effectivedate)
        {
            ICategoryTitle = title;
            ICategoryDescription = description;
            ICategoryUrl = website;
            IEffectiveDate = effectivedate;
        }

        public Instructor_Category()
        {
        }
    }
}
