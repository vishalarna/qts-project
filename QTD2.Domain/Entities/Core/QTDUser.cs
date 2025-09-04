using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class QTDUser : Common.Entity
    {
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<AdminMessage_QTDUser> AdminMessage_QTDUsers { get; set; } = new List<AdminMessage_QTDUser>();
        public virtual ICollection<SimulatorScenario_Collaborator> SimulatorScenario_Collaborators { get; set; } = new List<SimulatorScenario_Collaborator>();
        public QTDUser(int personId)
        {
            PersonId = personId;
        }

        public QTDUser()
        {
        }

        public void setPersonId(int personId)
        {
            PersonId = personId;
        }
    }
}
