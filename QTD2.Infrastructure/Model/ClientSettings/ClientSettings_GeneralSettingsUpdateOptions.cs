using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientSettings
{
    public class ClientSettings_GeneralSettingsUpdateOptions
    {
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string DateFormat { get; set; }
        public string ClassStartEndTimeFormat { get; set; }
        public decimal CompanySpecificCoursePassingScore { get; set; }
        public string DefaultTimeZone { get; set; }
    }
}
