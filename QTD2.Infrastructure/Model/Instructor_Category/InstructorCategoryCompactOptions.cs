using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Instructor;
using QTD2.Infrastructure.Model.Instructor_Category;

namespace QTD2.Infrastructure.Model.Instructor_Category
{
    public class InstructorCategoryCompactOptions
    {
        public QTD2.Domain.Entities.Core.Instructor_Category Instructor_Category { get; set; }

        public List<InstructorCompactOptions> InstructorCompactOptions { get; set; } = new List<InstructorCompactOptions>();
    }
}
