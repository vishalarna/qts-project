using QTD2.Domain.Entities.Common;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_CollaboratorPermission : Entity
    {
        public string Permission { get; set; }

        public virtual ICollection<SimulatorScenario_Collaborator> SimulatorScenario_Collaborators { get; set; } = new List<SimulatorScenario_Collaborator>();

        public SimulatorScenario_CollaboratorPermission(string permission)
        {
            Permission = permission;
        }

        public SimulatorScenario_CollaboratorPermission()
        {

        }

    }
}
