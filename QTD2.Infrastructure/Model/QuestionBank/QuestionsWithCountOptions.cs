using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.QuestionBank
{
    public class QuestionsWithCountOptions
    {
        public string Stem { get; set; }

        public int Id { get; set; }
        public string QuestionId { get; set; }

        public bool Active { get; set; }
        public QuestionsWithCountOptions()
        {
                
        }
        public QuestionsWithCountOptions(string stem,int id,string questionId,bool active)
        {
            Stem = stem;
            Id = id;
            QuestionId = questionId;
            Active = active;
        }
    }
}
