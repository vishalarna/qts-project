using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.StudentEvaluation
{
    public class StudentEvaluationVM
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public string StudentEvaluationId { get; set; }
        public bool? IsPublished { get; set; }
        public int questionsNum { get; set; }

        public int ClassRoaster { get; set; }

        public bool ClassRoasterIsReleased { get; set; }



        public StudentEvaluationVM()
        {
                
        }
        public StudentEvaluationVM(string studentEvaluationId, string title, int id,bool? isPublished, bool active, int classRoaster, bool classRoasterIsReleased)
        {
            Title = title; 
            Id = id;
            StudentEvaluationId = studentEvaluationId;
            Active = active;
            IsPublished = isPublished;
            ClassRoaster = classRoaster;
            ClassRoasterIsReleased = ClassRoasterIsReleased;
        }
    }
}
