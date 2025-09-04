using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class CBT_ScormUpload : Common.Entity
    {
        public int Id { get; set; }
        public int CbtId { get; set; }
        public string Name { get; set; }
        public string ScormStatus { get; set; }
        public DateTime ConnectedDate { get; set; }
        public DateTime? DisconnectedDate { get; set; }
        public virtual CBT CBT { get; set; }
        public virtual ICollection<CBT_ScormRegistration> CBT_ScormRegistration { get; set; } = new List<CBT_ScormRegistration>();
        public virtual ICollection<CBT_ScormUpload_Question> CBT_ScormUpload_Question { get; set; } = new List<CBT_ScormUpload_Question>();

        public CBT_ScormUpload(int cbtId, string name)
        {
            CbtId = cbtId;
            Name = name;
        }
        public CBT_ScormUpload() { }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetScormStatus(string status)
        {
            ScormStatus = status;
        }
        public void Connect()
        {
            AddDomainEvent(new Domain.Events.Core.OnCBT_ScormUpload_Connect(this));
            ConnectedDate = DateTime.Now;
            Activate();
        }

        public void Disconnect()
        {
            AddDomainEvent(new Domain.Events.Core.OnCBT_ScormUpload_Disconnect(this));
            DisconnectedDate = DateTime.Now;
            ScormStatus = "Disconnected";
            Deactivate();
        }

        public void MarkAsUploaded()
        {
            ScormStatus = "Uploaded";
        }

        public CBT_ScormUpload_Question AddQuestion(CBT_ScormUpload_Question_Type type, string questionId, string description, List<string> choices, List<string> correctChoices) 
        {
            if (CBT_ScormUpload_Question == null) CBT_ScormUpload_Question = new List<CBT_ScormUpload_Question>();

            var question = CBT_ScormUpload_Question.Where(r => r.QuestionId == questionId).FirstOrDefault();

            if (question == null)
            {
                question = new CBT_ScormUpload_Question(this, type, questionId, description, choices, correctChoices);

                CBT_ScormUpload_Question.Add(question);
            }
            else
            {
                foreach(var choice in choices)
                {
                    question.AddChoice(choice, correctChoices.Contains(choice));
                }
            }

            return question;
        }
    }

}
