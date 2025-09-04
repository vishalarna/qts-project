using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.QuestionBank
{
    public class QuestionBankCustomModel
    {
        public string Stem { get; set; }
        public string questionId { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public QuestionBankCustomModel()
        {

        }
        public QuestionBankCustomModel(string stem, int id)
        {
            Stem = stem;
            Id = id;
        }
    }
}
