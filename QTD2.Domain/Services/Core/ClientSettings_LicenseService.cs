using QTD2.Domain.Entities.Core;
using QTD2.Domain.Exceptions;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using QTD2.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class ClientSettings_LicenseService : Common.Service<ClientSettings_License>, IClientSettings_LicenseService {
        private readonly IEmployeeService _employeeService;

        public ClientSettings_LicenseService(IClientSettings_LicenseRepository repository, IClientUserSettings_LicenseValidation validation, IEmployeeService employeeService)
              : base(repository, validation)
        {
            _employeeService = employeeService;
        }

        public async System.Threading.Tasks.Task<ClientSettings_License> GetCurrentLicense()
        {
            var current = await FindAsync(r => r.Active);

            if (current.Count() > 1) throw new QTDServerException("License Validation Error: More than one current license");
            var currentLicense = current.FirstOrDefault();
            if (currentLicense != null)
            {
                var employees = await _employeeService.GetActiveEmployeesAsync();

                currentLicense.SetEmployeeRecordsUsed(employees.Count());
                currentLicense.InterpretActivationCode();
            }

            return current.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<ClientSettings_License>> GetLicenseHistoryAsync()
        {

            var licenseHistory = await AllAsync();

            foreach(var item in licenseHistory)
            {
                item.InterpretActivationCode();
                    var employees = await _employeeService.GetActiveEmployeesAsync();
                    item.SetEmployeeRecordsUsed(employees.Count());
            }
            return licenseHistory.ToList();
        }
    }
}
