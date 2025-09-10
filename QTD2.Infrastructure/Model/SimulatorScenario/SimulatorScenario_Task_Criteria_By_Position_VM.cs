using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_Task_Criteria_By_Position_VM
    {        
        public int? Id {  get; set; }
        public int TaskId {  get; set; }
        public string PositionAbbreviation {  get; set; }
        public string CompleteTaskNumber { get; set; }
        public string Description {  get; set; }
        public string Criteria { get; set; }

        public SimulatorScenario_Task_Criteria_By_Position_VM()
        {
            
        }

        public SimulatorScenario_Task_Criteria_By_Position_VM(int? id, int taskId, string positionAbbreviation, string completeTaskNumber, string description, string criteria)
        {
            Id = id;
            TaskId = taskId;
            PositionAbbreviation = positionAbbreviation;
            CompleteTaskNumber = completeTaskNumber;
            Description = description;
            Criteria = criteria;
        }
    }
}
