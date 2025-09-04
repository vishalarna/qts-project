using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_Position_VM
    {
        public int? Id { get; set; }
        public int PositionId { get; set; }
        public string PositionTitle { get; set; }

        public SimulatorScenario_Position_VM(int? id, int positionId, string positionTitle)
        {
            Id = id;
            PositionId = positionId;
            PositionTitle = positionTitle;
        }
    }
}
