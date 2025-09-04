using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor
{
    public class Instructor_CreateOptions
    {
        public int ICategoryId { get; set; }

        public string Num { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public bool Isworkbookadmin { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string InstructorNotes { get; set; }
        public Instructor_CreateOptions()
        {

        }
        public Instructor_CreateOptions(int categoryId, string instructorNumber, string email, string description, string name)
        {
            ICategoryId = categoryId;
            Num = instructorNumber;
            Email = email;
            Description = description;
            Name = name;
        }
    }
}
