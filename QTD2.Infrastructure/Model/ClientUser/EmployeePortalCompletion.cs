using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientUser
{
    public class EmployeePortalCompletion
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string CompletionName { get; set; }

        public EmployeePortalCompletion(string name, DateTime date, string type, string completionName)
        {
            Name = name;
            Date = date;
            Type = type;
            CompletionName = completionName;
        }
    }
}
