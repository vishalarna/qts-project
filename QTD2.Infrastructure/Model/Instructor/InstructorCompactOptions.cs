using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Instructor
{
    public class InstructorCompactOptions
    {
        public InstructorCompactOptions(int id, int? insCategoryId, string title, bool active, string number, string orderProperty)
        {
            Id = id;
            InstructorCategoryId = insCategoryId;
            Title = title;
            Active = active;
            Number = number;
            OrderProperty = orderProperty;
        }

        public int Id { get; set; }

        public int? InstructorCategoryId { get; set; }

        public string Title { get; set; }

        public string Number { get; set; }

        public string OrderProperty { get; set; }

        public bool Active { get; set; }
    }
}
