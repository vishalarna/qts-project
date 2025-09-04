using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario_CollaboratorPermission
{
    public class SimulatorScenario_CollaboratorPermissions_VM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SimulatorScenario_CollaboratorPermissions_VM(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public SimulatorScenario_CollaboratorPermissions_VM() { }
    }
}
