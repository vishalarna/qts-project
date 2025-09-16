using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SimulatorScenario
{
    public class SimulatorScenario_Procedure_VM
    {
        public int? Id { get; set; }
        public int ProcedureId { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public SimulatorScenario_Procedure_VM(int id, int procedureId, string number, string description, string title)
        {
            Id = id;
            ProcedureId = procedureId;
            Number = number;
            Description = description;
            Title = title;
        }

        public SimulatorScenario_Procedure_VM() { }
    }
}
