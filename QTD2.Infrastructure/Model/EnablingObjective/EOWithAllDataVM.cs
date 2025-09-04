using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;
using QTD2.Infrastructure.Model.TestItem;

namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EOWithAllDataVM
    {
        public List<ILAWithCountOptions> IlasWithCount { get; set; } = new List<ILAWithCountOptions>();

        public List<TaskWithCountOptions> TasksWithCount { get; set; } = new List<TaskWithCountOptions>();

        public List<ProceduresWithLinkCount> ProceduresWithCount { get; set; } = new List<ProceduresWithLinkCount>();

        public List<RegulatoryRequirementWithLinkCount> RRsWithCount { get; set; } = new List<RegulatoryRequirementWithLinkCount>();

        public List<SafetyHazardWithLinkCount> SHsWithCount { get; set; } = new List<SafetyHazardWithLinkCount>();

        public List<TestItemWithTestCount> TIsWithCount { get; set; } = new List<TestItemWithTestCount>();

        public List<TaskPositionWithCount> PositionsWithCount { get; set; } = new List<TaskPositionWithCount>();

        public List<EmployeeWithCountOptions> EmployeesWithCount { get; set; } = new List<EmployeeWithCountOptions>();
    }
}
