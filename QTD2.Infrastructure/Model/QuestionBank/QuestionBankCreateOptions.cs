using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.QuestionBank
{
    public class QuestionBankCreateOptions
    {
        public string Stem { get; set; }
        public string Mode { get; set; }

        public List<string> stemArray { get; set; }

        public int studentEvaluationId { get; set; }
        QuestionBankCreateOptions()
        {

        }
    }
}
