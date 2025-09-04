using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.CSE_ILACertLink_PartialCredit
{
    public class CSE_ILACertPartialCreditCreateUpdateOption
    {
        public List<CSE_ILACertPartialCredit> CSE_ILACertPartialCredits = new List<CSE_ILACertPartialCredit>();
    }

    public class CSE_ILACertPartialCredit
    {
        public int ClassScheduleEmployeeId { get; set; }
        public double? PartialCreditHours { get; set; }
        public List<CSE_ILACertSubRequirementPartialCredit> subRequirements = new List<CSE_ILACertSubRequirementPartialCredit>();
    }

    public class CSE_ILACertSubRequirementPartialCredit
    {
        public string Reqname { get; set; }
        public double? PartialCreditHours { get; set; }
    }
}
