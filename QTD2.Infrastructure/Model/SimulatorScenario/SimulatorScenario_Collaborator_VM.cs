using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_Collaborator_VM
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CollaboratorPermissionId { get; set; }

        public SimulatorScenario_Collaborator_VM(int? id, int userId, string name, string email, int collaboratorPermissionId)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Email = email;
            CollaboratorPermissionId = collaboratorPermissionId;
        }

        public SimulatorScenario_Collaborator_VM() { }
    }
}
