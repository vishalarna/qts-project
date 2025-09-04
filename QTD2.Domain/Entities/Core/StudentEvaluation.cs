using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class StudentEvaluation : Common.Entity
    {
        public int RatingScaleId { get; set; }

        public string Title { get; set; }

        public string Instructions { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsAvailableForAllILAs { get; set; }

        public bool? IsAvailableForSelectedILAs { get; set; }

        public bool? IsIncludeCommentSections{ get; set; }

        public bool? IsAllowNAOption { get; set; }

        public virtual RatingScaleN RatingScaleN { get; set; }

        public virtual ICollection<StudentEvaluationHistory> StudentEvaluationHistories { get; set; } = new List<StudentEvaluationHistory>();
        public virtual ICollection<ClassSchedule_Evaluation_Roster> ClassSchedule_Evaluation_Rosters { get; set; } = new List<ClassSchedule_Evaluation_Roster>();

        public virtual ICollection<StudentEvaluation_Question> StudentEvaluationQuestions { get; set; } = new List<StudentEvaluation_Question>();

        public virtual ICollection<ILA_StudentEvaluation_Link> ILA_StudentEvaluation_Links { get; set; } = new List<ILA_StudentEvaluation_Link>();

        public virtual ICollection<ClassSchedule_StudentEvaluations_Link> ClassSchedule_StudentEvaluations_Links { get; set; } = new List<ClassSchedule_StudentEvaluations_Link>();

        public virtual List<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; } = new List<StudentEvaluationWithoutEmp>();
        public StudentEvaluation()
        {
                
        }

        public StudentEvaluation(int ratingScaleId,string title, string instructions, bool? isPublished, bool? isAvailableForAllILAs, bool? isIncludeCommentSections, bool? isAllowNAOption, bool? isAvailableForSelectedILAs)
        {
            RatingScaleId = ratingScaleId;
            Title = title;
            Instructions = instructions;
            IsPublished = isPublished;
            IsAvailableForAllILAs = isAvailableForAllILAs;
            IsIncludeCommentSections = isIncludeCommentSections;
            IsAllowNAOption = isAllowNAOption;
            IsAvailableForSelectedILAs = isAvailableForSelectedILAs;
        }
        public StudentEvaluation_Question LinkQuestion(QuestionBank questionBank)
        {
            StudentEvaluation_Question studentEvaluation_question_link = StudentEvaluationQuestions.FirstOrDefault(x => x.QuestionBankId == questionBank.Id && x.StudentEvaluationId == this.Id);
            if (studentEvaluation_question_link != null)
            {
                return studentEvaluation_question_link;
            }

            studentEvaluation_question_link = new StudentEvaluation_Question(this, questionBank);
            AddEntityToNavigationProperty<StudentEvaluation_Question>(studentEvaluation_question_link);
            return studentEvaluation_question_link;
        }
        public void UnlinkQuestion(QuestionBank questionBank)
        {
            StudentEvaluation_Question studentEvaluation_question_link = StudentEvaluationQuestions.FirstOrDefault(x => x.QuestionBankId == questionBank.Id);
            if (studentEvaluation_question_link != null)
            {
                RemoveEntityFromNavigationProperty<StudentEvaluation_Question>(studentEvaluation_question_link);
            }
        }


        public ClassSchedule_StudentEvaluations_Link LinkClassSchedule(ClassSchedule classSchedule)
        {
            ClassSchedule_StudentEvaluations_Link classSchedule_studentEvaluation_link = ClassSchedule_StudentEvaluations_Links.FirstOrDefault(x => x.ClassScheduleId == classSchedule.Id && x.StudentEvaluationId == this.Id);
            if (classSchedule_studentEvaluation_link != null)
            {
                return classSchedule_studentEvaluation_link;
            }
            classSchedule_studentEvaluation_link = new ClassSchedule_StudentEvaluations_Link(this, classSchedule);
            AddEntityToNavigationProperty<ClassSchedule_StudentEvaluations_Link>(classSchedule_studentEvaluation_link);
            return classSchedule_studentEvaluation_link;
        }

        //public override T Copy<T>(string createdBy)
        //{
        //    var studentEvaluationCopy = base.Copy<T>(createdBy) as StudentEvaluation;

        //    //public virtual ICollection<StudentEvaluation_Question> StudentEvaluationQuestions { get; set; } = new List<StudentEvaluation_Question>();
        //    foreach (var studentEvaluationQuestions in this.StudentEvaluationQuestions)
        //    {
        //        var studentEvaluationQuestionsCopy = studentEvaluationQuestions.Copy<StudentEvaluation_Question>(createdBy);
        //        studentEvaluationQuestionsCopy.StudentEvaluationId = 0;
        //        studentEvaluationCopy.StudentEvaluationQuestions.Add(studentEvaluationQuestionsCopy);
        //    }

        //    return (T)(object)studentEvaluationCopy;
        //}

    }
}
