using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClientSettings
{
    public class ClientSettings_LicenseVM
    {
        public string ActivationCode { get; set; }
        public int ClientAccountNumber { get; set; }
        public DateTime Expiration { get; set; }
        public string LicenseType { get; set; }
        public int TotalEmployeeRecordsAvailable { get; set; }
        public int EmployeeRecordsUsed { get; set; }
        public bool HasEmp { get; set; }

        public bool IsLicenseActiveAndValid { get; set;}
        public int RemainingEmployees { get; set; }
        public bool Deluxe { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<ClientSettings_License_ProductInfo> Products { get; set; }
    }    
}
