using System;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class QuestionBankHistory : Entity
    {
        public int QuestionBankId { get; set; }

        public string QuestionBankNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public virtual QuestionBank QuestionBank { get; set; }

        public QuestionBankHistory()
        {

        }
        public QuestionBankHistory(int questionBankId, string questionBankNotes, DateTime? effectiveDate)
        {
            QuestionBankId = questionBankId;
            QuestionBankNotes = questionBankNotes;
            EffectiveDate = effectiveDate;
        }
    }

}