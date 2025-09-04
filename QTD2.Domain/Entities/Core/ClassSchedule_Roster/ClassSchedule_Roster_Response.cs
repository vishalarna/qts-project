using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Roster_Response : Common.Entity
    {
        public int ClassScheduleRosterId { get; set; }
        public int TestItemId { get; set; }
        public bool? IsCorrect { get; set; }
        public bool IsComplete { get; set; }

        public virtual ClassSchedule_Roster ClassSchedule_Roster { get; set; }
        public virtual TestItem TestItem { get; set; }

        public virtual ICollection<ClassSchedule_Roster_Response_Selection> Selections { get; set; }


        public ClassSchedule_Roster_Response()
        {

        }

        public ClassSchedule_Roster_Response(int classScheduleRosterId, int testItemid, bool? isCorrect)
        {
            ClassScheduleRosterId = classScheduleRosterId;
            TestItemId = testItemid;
            IsCorrect = isCorrect;
        }

        public void LoadSelection(string userAnswer)
        {
            if (Selections == null) Selections = new List<ClassSchedule_Roster_Response_Selection>();

            Selections.Add(new ClassSchedule_Roster_Response_Selection()
            {
                Active = true,
                UserAnswer = userAnswer,
                CreatedDate = DateTime.Now,
                Deleted = false,
                ClassScheduleRosterResponseId = this.Id
            });
        }

        public void LoadSelection(string userAnswer, int correctIndex)
        {
            if (Selections == null) Selections = new List<ClassSchedule_Roster_Response_Selection>();

            Selections.Add(new ClassSchedule_Roster_Response_Selection()
            {
                Active = true,
                UserAnswer = userAnswer,
                CreatedDate = DateTime.Now,
                Deleted = false,
                ClassScheduleRosterResponseId = this.Id,
                CorrectIndex = correctIndex
            });
        }

        public void LoadSelection(string userAnswer, string matchValue)
        {
            if (Selections == null) Selections = new List<ClassSchedule_Roster_Response_Selection>();

            Selections.Add(new ClassSchedule_Roster_Response_Selection()
            {
                Active = true,
                UserAnswer = userAnswer,
                CreatedDate = DateTime.Now,
                Deleted = false,
                ClassScheduleRosterResponseId = this.Id,
                MatchValue = matchValue
            });
        }

        public void LoadSelection(string userAnswer, string matchValue, int correctIndex)
        {
            if (Selections == null) Selections = new List<ClassSchedule_Roster_Response_Selection>();

            Selections.Add(new ClassSchedule_Roster_Response_Selection()
            {
                Active = true,
                UserAnswer = userAnswer,
                CreatedDate = DateTime.Now,
                Deleted = false,
                ClassScheduleRosterResponseId = this.Id,
                MatchValue = matchValue,
                CorrectIndex = correctIndex
            });
        }

        public override void Create(string username)
        {
            base.Create(username);

            if (Selections == null) return;

            foreach (var select in Selections)
            {
                select.Create(username);
            }
        }

        public override void Modify(string username)
        {
            base.Modify(username);

            if (Selections == null) return;

            foreach (var select in Selections)
            {
                select.Modify(username);
            }
        }

        public void MarkAsIncomplete()
        {
            this.IsComplete = false;
            this.IsCorrect = false;
        }

        public void MarkAsComplete(bool isCorrect)
        {
            this.IsComplete = true;
            this.IsCorrect = isCorrect;
        }

        public void ClearSelection()
        {
            if (Selections == null) return;

            foreach (var select in Selections)
            {
                select.Delete();
            }
        }
    }
}
