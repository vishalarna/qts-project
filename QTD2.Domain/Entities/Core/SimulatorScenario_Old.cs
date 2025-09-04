using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class SimulatorScenario_Old : Entity
    {
        public int SimScenarioDiffID { get; set; }

        public string SimScenarioTitle { get; set; }

        public string SimScenarioDesc { get; set; }

        public int SimScenarioDurationHours { get; set; }

        public int SimScenarioDurationMins { get; set; }

        public byte[] SimScenarioUpload { get; set; }

        public virtual SimulatorScenarioDifficultyLevel_Old SimulatorScenarioDifficultyLevel { get; set; }

        public virtual ICollection<SimulatorScenarioILA_Link_Old> SimulatorScenarioILA_Links { get; set; } = new List<SimulatorScenarioILA_Link_Old>();

        public virtual ICollection<SimulatorScenarioPositon_Link_Old> SimulatorScenarioPositon_Links { get; set; } = new List<SimulatorScenarioPositon_Link_Old>();

        public virtual ICollection<SimulatorScenario_EnablingObjectives_Link_Old> SimulatorScenario_EnablingObjectives_Links { get; set; } = new List<SimulatorScenario_EnablingObjectives_Link_Old>();

        public virtual ICollection<SimulatorScenarioTaskObjectives_Link_Old> SimulatorScenarioTaskObjectives_Links { get; set; } = new List<SimulatorScenarioTaskObjectives_Link_Old>();

        public SimulatorScenario_Old()
        {
        }

        public SimulatorScenario_Old(int simScenarioDiffID, string simScenarioTitle, string simScenarioDesc, int simScenarioDurationHours, int simScenarioDurationMins, byte[] simScenarioUpload)
        {
            SimScenarioDiffID = simScenarioDiffID;
            SimScenarioTitle = simScenarioTitle;
            SimScenarioDesc = simScenarioDesc;
            SimScenarioDurationHours = simScenarioDurationHours;
            SimScenarioDurationMins = simScenarioDurationMins;
            SimScenarioUpload = simScenarioUpload;
        }

        public SimulatorScenarioILA_Link_Old LinkSimulatorScenarioILA(ILA ila)
        {
            SimulatorScenarioILA_Link_Old simulatorScenarioILA_Link = SimulatorScenarioILA_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.ILAID == ila.Id);
            if (simulatorScenarioILA_Link != null)
            {
                return simulatorScenarioILA_Link;
            }

            simulatorScenarioILA_Link = new SimulatorScenarioILA_Link_Old(ila, this);
            AddEntityToNavigationProperty<SimulatorScenarioILA_Link_Old>(simulatorScenarioILA_Link);
            return simulatorScenarioILA_Link;
        }

        public void UnLinkSimulatorScenarioILA(ILA ila)
        {
            SimulatorScenarioILA_Link_Old simulatorScenarioILA_Link = SimulatorScenarioILA_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.ILAID == ila.Id);
            if (simulatorScenarioILA_Link != null)
            {
                RemoveEntityFromNavigationProperty<SimulatorScenarioILA_Link_Old>(simulatorScenarioILA_Link);
            }
        }

        public SimulatorScenarioPositon_Link_Old LinkSimulatorScenarioPositon(Position position)
        {
            SimulatorScenarioPositon_Link_Old simulatorScenarioPositon_Link = SimulatorScenarioPositon_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.PositionID == position.Id);
            if (simulatorScenarioPositon_Link != null)
            {
                return simulatorScenarioPositon_Link;
            }

            simulatorScenarioPositon_Link = new SimulatorScenarioPositon_Link_Old(this, position);
            AddEntityToNavigationProperty<SimulatorScenarioPositon_Link_Old>(simulatorScenarioPositon_Link);
            return simulatorScenarioPositon_Link;
        }

        public void UnLinkSimulatorScenarioPositon(Position position)
        {
            SimulatorScenarioPositon_Link_Old simulatorScenarioPositon_Link = SimulatorScenarioPositon_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.PositionID == position.Id);
            if (simulatorScenarioPositon_Link != null)
            {
                RemoveEntityFromNavigationProperty<SimulatorScenarioPositon_Link_Old>(simulatorScenarioPositon_Link);
            }
        }

        public SimulatorScenario_EnablingObjectives_Link_Old LinkSimulatorScenarioEO(EnablingObjective enablingObjective)
        {
            SimulatorScenario_EnablingObjectives_Link_Old simulatorScenario_EnablingObjectives_Link = SimulatorScenario_EnablingObjectives_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.EnablingObjectiveID == enablingObjective.Id);
            if (simulatorScenario_EnablingObjectives_Link != null)
            {
                return simulatorScenario_EnablingObjectives_Link;
            }

            simulatorScenario_EnablingObjectives_Link = new SimulatorScenario_EnablingObjectives_Link_Old(this, enablingObjective);
            AddEntityToNavigationProperty<SimulatorScenario_EnablingObjectives_Link_Old>(simulatorScenario_EnablingObjectives_Link);
            return simulatorScenario_EnablingObjectives_Link;
        }

        public void UnLinkSimulatorScenarioEO(EnablingObjective enablingObjective)
        {
            SimulatorScenario_EnablingObjectives_Link_Old simulatorScenario_EnablingObjectives_Link = SimulatorScenario_EnablingObjectives_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.EnablingObjectiveID == enablingObjective.Id);
            if (simulatorScenario_EnablingObjectives_Link != null)
            {
                RemoveEntityFromNavigationProperty<SimulatorScenario_EnablingObjectives_Link_Old>(simulatorScenario_EnablingObjectives_Link);
            }
        }

        public SimulatorScenarioTaskObjectives_Link_Old LinkSimulatorScenarioTask(Task task)
        {
            SimulatorScenarioTaskObjectives_Link_Old simulatorScenarioTaskObjectives_Link = SimulatorScenarioTaskObjectives_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.TaskID == task.Id);
            if (simulatorScenarioTaskObjectives_Link != null)
            {
                return simulatorScenarioTaskObjectives_Link;
            }

            simulatorScenarioTaskObjectives_Link = new SimulatorScenarioTaskObjectives_Link_Old(this, task);
            AddEntityToNavigationProperty<SimulatorScenarioTaskObjectives_Link_Old>(simulatorScenarioTaskObjectives_Link);
            return simulatorScenarioTaskObjectives_Link;
        }

        public void UnLinkSimulatorScenarioTask(Task task)
        {
            SimulatorScenarioTaskObjectives_Link_Old simulatorScenarioTaskObjectives_Link = SimulatorScenarioTaskObjectives_Links.FirstOrDefault(x => x.SimulatorScenarioID == this.Id && x.TaskID == task.Id);
            if (simulatorScenarioTaskObjectives_Link != null)
            {
                RemoveEntityFromNavigationProperty<SimulatorScenarioTaskObjectives_Link_Old>(simulatorScenarioTaskObjectives_Link);
            }
        }
    }
}
