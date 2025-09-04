using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluationWithEMP
{
    public class StudentEvaluationWithEMPVM
    {
        public int EmpId { get; set; }

        public int ClassId { get; set; }

        public int EvaluationId { get; set; }

        public string EmpName { get; set; }

        public string EmpImage { get; set; }

        public string EmpEmail { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool IsReleased { get; set; }

        public bool HasAggregateData { get; set; }

        public List<RatingList> Rating { get; set; } = new List<RatingList>();
    }

    public class RatingList
    {
        public int? QuestionId { get; set; }

        public int? Rating { get; set; }
    }
}
