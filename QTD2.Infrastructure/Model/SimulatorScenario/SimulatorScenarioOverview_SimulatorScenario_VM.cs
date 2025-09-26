using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenarioOverview_SimulatorScenario_VM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ILAs { get; set; }
        public string Positions { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public string Difficulty { get; set; }
        public SimulatorScenario_CollaboratorPermissions_VM CurrentUserPermissions { get; set; }
        public List<SimulatorScenario_Collaborator_VM> Collaborators { get; set; } = new List<SimulatorScenario_Collaborator_VM>();
        public List<int> ProviderIds { get; set; } = new List<int>();

        public SimulatorScenarioOverview_SimulatorScenario_VM(int id, string title, string iLAs, string positions, string status, bool active, string difficulty, List<int> providerIds)
        {
            Id = id;
            Title = title;
            ILAs = iLAs;
            Positions = positions;
            Status = status;
            Active = active;
            Difficulty = difficulty;
            ProviderIds = providerIds;
        }

        public SimulatorScenarioOverview_SimulatorScenario_VM() { }
        public void setCollaborators(List<SimulatorScenario_Collaborator> collaborators,int personId)
        {
            var currentUserCollaborator = collaborators.FirstOrDefault(collab => collab.User.PersonId == personId);
            CurrentUserPermissions = currentUserCollaborator != null ? new SimulatorScenario_CollaboratorPermissions_VM( currentUserCollaborator.Permission?.Id ?? 0,currentUserCollaborator.Permission?.Permission ?? null) : null;

            foreach (var collab in collaborators)
            {
                var collab_VM = new SimulatorScenario_Collaborator_VM(collab.Id,collab.UserId, $"{collab.User.Person.FirstName} {collab.User.Person.LastName}",collab.User.Person.Username,collab.PermissionId);
                Collaborators.Add(collab_VM);
            }
        }
    }
}
