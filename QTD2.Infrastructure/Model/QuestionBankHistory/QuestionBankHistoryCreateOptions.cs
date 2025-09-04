using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.QuestionBankHistory
{
    public class QuestionBankHistoryCreateOptions
    {
        public int QuestionBankId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string QuestionBankNotes { get; set; }

        public string ActionType { get; set; }
    }
}
