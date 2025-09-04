using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Notification.Content.Models
{
  public  class SimulatorScenarioCollaborationModel
    {
        public string CollaboratorFirstName { get; set; }
        public string CollaboratorLastName { get; set; }
        public string SimulatorScenarioTitle { get; set; }
        public string SimulatorScenarioLink { get; set; }
        public string SimulatorScenarioMessage { get; set; }


        public SimulatorScenarioCollaborationModel(string collaboratorFirstName, string collaboratorLastName, string simulatorScenarioTitle, string simulatorScenarioLink, string simulatorScenarioMessage)
        {
            CollaboratorFirstName = collaboratorFirstName;
            CollaboratorLastName = collaboratorLastName;
            SimulatorScenarioTitle = simulatorScenarioTitle;
            SimulatorScenarioLink = simulatorScenarioLink;
            SimulatorScenarioMessage = simulatorScenarioMessage;
        }

        public SimulatorScenarioCollaborationModel()
        {

        }
    }


}
