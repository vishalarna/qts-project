using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_StudentEvaluation_Link : Entity
    {
        public int ILAId { get; set; }

        public int studentEvalFormID { get; set; }

        public int? studentEvalAvailabilityID { get; set; }

        public int? studentEvalAudienceID { get; set; }

        public bool? isAllQuestionMandatory { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual StudentEvaluationAvailability StudentEvaluationAvailability { get; set; }

        public virtual StudentEvaluationAudience StudentEvaluationAudience { get; set; }

        public virtual StudentEvaluation StudentEvaluationForm { get; set; }

        public ILA_StudentEvaluation_Link()
        {
        }

        public ILA_StudentEvaluation_Link(ILA iLA, StudentEvaluation studentEvaluationForm, StudentEvaluationAvailability studentEvaluationAvailability, StudentEvaluationAudience studentEvaluationAudience)
        {
            ILA = iLA;
            StudentEvaluationForm = studentEvaluationForm;
            StudentEvaluationAvailability = studentEvaluationAvailability;
            StudentEvaluationAudience = studentEvaluationAudience;
            this.ILAId = iLA.Id;
            this.studentEvalFormID = studentEvaluationForm.Id;
            this.studentEvalAvailabilityID = studentEvaluationAvailability.Id;
            this.studentEvalAudienceID = studentEvaluationAudience.Id;
        }
        public ILA_StudentEvaluation_Link(ILA iLA, StudentEvaluation studentEvaluationForm)
        {
            ILA = iLA;
            StudentEvaluationForm = studentEvaluationForm;
            this.ILAId = iLA.Id;
            this.studentEvalFormID = studentEvaluationForm.Id;           
        }

        //public override T Copy<T>(string createdBy)
        //{
        //    var iLA_StudentEvaluation_LinkCopy = base.Copy<T>(createdBy) as ILA_StudentEvaluation_Link;

        //    var studentEvaluationFormCopy = this.StudentEvaluationForm.Copy<StudentEvaluation>(createdBy);
        //    iLA_StudentEvaluation_LinkCopy.studentEvalFormID = 0;
        //    iLA_StudentEvaluation_LinkCopy.StudentEvaluationForm = studentEvaluationFormCopy;


        //    return (T)(object)iLA_StudentEvaluation_LinkCopy;
        //}

        public void AddClassScheduleEvalLinks()
        {
            AddDomainEvent(new Domain.Events.Core.OnILA_StudentEvaluation_Link_Added(this));
        }

        public void RemoveClassScheduleEvalLinks()
        {
            AddDomainEvent(new Domain.Events.Core.OnILA_StudentEvaluation_Link_Removed(this));
        }
    }
}
