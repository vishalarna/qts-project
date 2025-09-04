using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Collaborator: Entity
    {
        public int SimulatorScenarioId { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public virtual SimulatorScenario SimulatorScenario { get; set; }
        public virtual QTDUser User { get; set; }
        public virtual SimulatorScenario_CollaboratorPermission Permission { get; set; }

        public SimulatorScenario_Collaborator()
        {

        }

        public SimulatorScenario_Collaborator(SimulatorScenario simulatorScenario, QTDUser user, SimulatorScenario_CollaboratorPermission permission)
        {
            SimulatorScenario = simulatorScenario;
            User = user;
            Permission = permission;
            SimulatorScenarioId = simulatorScenario.Id;
            UserId = user.Id;
            PermissionId = permission.Id;
        }

        public void SetPermission(int permissionId)
        {
            PermissionId = permissionId;
        }
    }
}
