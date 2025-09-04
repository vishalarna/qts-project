using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_License_ProductInfo : Entity
    {
        public int ClientSettings_LicenseId { get; set; }
        public string ProductName { get; set; }
        public string ProductAcronym { get { return getProductAcronym(); } }
        public string Version { get; set; }
        public string ReleaseDate { get { return getReleaseDate(); } }
        public bool Enabled { get; set; }

        public ClientSettings_License_ProductInfo() { }

        public ClientSettings_License_ProductInfo(int licenseId, string productName, string version, bool enabled)
        {
            ClientSettings_LicenseId = licenseId;
            ProductName = productName;
            Version = version;
            Enabled = enabled;
        }

        private string getReleaseDate()
        {
            switch (Version)
            {
                default: 
                    return "TBD";
            }
        }

        protected string getProductAcronym()
        {
            switch (ProductName.ToUpper())
            {
                case "TEST DEVELOPMENT TOOL":
                    return "TDT";
                case "EMPLOYEE PORTAL":
                    return "EMP";
                case "INSTRUCTOR WORKBOOK":
                    return "IWP";
                case "MANAGER PORTAL":
                    return "MP";
                default:
                    return "UNK";
            }
        }
    }
}
