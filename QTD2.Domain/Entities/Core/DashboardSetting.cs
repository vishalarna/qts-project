using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class DashboardSetting : Common.Entity
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
    }
}
