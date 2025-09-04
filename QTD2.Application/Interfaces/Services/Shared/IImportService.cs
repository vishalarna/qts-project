using Microsoft.AspNetCore.Http;
using QTD2.Infrastructure.Model.DIFSurveyEmployeeResponse;
using QTD2.Infrastructure.Model.Import_CSV_VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IImportService
    {
        public Task<ValidateCSV_DIFSurveyEmployeeResponse_Results_VM> ValidateDIFEmployeeResponseCSVFileAsync(ValidateCSV_DIFSurveyEmployeeResponse_VM options);
        public Task<ValidateCSV_ILA_Results_VM> ValidateILAAsync(ValidateCSV_VM options);
        public Task<ValidateCSV_Employee_Results_VM> ValidateEmployeeAsync(ValidateCSV_VM options);
        public Task<ValidateCSV_Class_Results_VM> ValidateClassAsync(ValidateCSV_VM options);
        public Task<ImportData_DIFSurveyEmployeeResponse_Results_VM> ImportDIFSurveyEmployeeResponseAsync(ImportData_DIFSurveyEmployeeResponse_VM options);
        public Task<ImportData_ILA_Results_VM> ImportILAAsync(ImportData_ILA_VM options);
        public Task<ImportData_Employee_Results_VM> ImportEmployeeAsync(ImportData_Employee_VM options);
        public Task<ImportData_Class_Results_VM> ImportClassAsync(ImportData_Class_VM options);
        public Task<byte[]> GetTemplateAsync(string type);
    }
}
