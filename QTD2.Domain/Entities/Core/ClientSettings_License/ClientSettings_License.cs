using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_License : Entity
    {

        protected int _licenseType;

        protected ILicenseInterpreter _licenseInterpreter;


        //The activation Code is what the client gets
        public string ActivationCode { get; set; }
        public int ClientId { get; set; }
        public DateTime Expiration { get; private set; }
        public string LicenseType { get; private set; }
        public int TotalEmployeeRecordsAvailable { get; private set; }
        public int EmployeeRecordsUsed { get; private set; }
        public bool HasEmp { get { return getHasEmp(); } }

        public bool IsLicenseActiveAndValid { get { return this.Active; } }
        public int RemainingEmployees { get { return TotalEmployeeRecordsAvailable - EmployeeRecordsUsed; } }
        public bool Deluxe { get { return LicenseType.ToUpper() == "DELUXE VERSION"; } }
        public List<ClientSettings_License_ProductInfo> Products { get; private set; }
        public List<Employee> Employees { get; set; }

        public ClientSettings_License() { }

        public ClientSettings_License(int clientId)
        {
            ClientId = clientId;
        }

        public ClientSettings_License(int clientId, string activationCode, List<Employee> employees)
        {
            ClientId = clientId;
            ActivationCode = activationCode;
            Employees = employees;

            InterpretActivationCode();
        }

        public void InterpretActivationCode()
        {
            _licenseInterpreter = new LicenseInterpreter(ClientId, ActivationCode);

            Expiration = _licenseInterpreter.GetExpiration();
            LicenseType = _licenseInterpreter.GetLicenseType();
            TotalEmployeeRecordsAvailable = _licenseInterpreter.GetMaxEmployees();

            Products = new List<ClientSettings_License_ProductInfo>();

            var hasTdt = _licenseInterpreter.GetHasTDT();
            var hasEmp = _licenseInterpreter.GetHasEMP();

            Products.Add(new ClientSettings_License_ProductInfo(this.Id, "Test Development Tool", "2.0", hasTdt));
            Products.Add(new ClientSettings_License_ProductInfo(this.Id, "Employee Portal", "2.0", hasEmp));
            Products.Add(new ClientSettings_License_ProductInfo(this.Id, "Instructor Workbook", "2.0", false));
            Products.Add(new ClientSettings_License_ProductInfo(this.Id, "Manager Portal", "2.0", false));
        }

        public bool Validate()
        {
            return true;
        }

        public void SetEmployeeRecordsUsed(int numberOfEmployees)
        {
            this.EmployeeRecordsUsed = numberOfEmployees;
        }

        private bool getHasEmp()
        {
            if (!this.Active) return false;

            if (this.Products == null) return false;

            var emp = this.Products.Where(r => r.ProductAcronym == "EMP").FirstOrDefault();

            if (emp == null) return false;

            return emp.Active && emp.Enabled;
        }
    }
}
