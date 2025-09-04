using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation.Models
{
    public class ClassCertificatesModel : IReportModel
    {
        public string Title { get; set; }
        public string TemplatePath { get; set; }
        public List<string> DisplayColumns { get; set; }
        public string CompanyLogo { get; set; }
        public string DefaultTimeZone { get; set; }
        public string DateFormat { get; set; }
        public List<Domain.Entities.Core.ClientSettings_LabelReplacement> ClientSettings_LabelReplacements { get; set; }
        public List<Domain.Entities.Core.ClassSchedule_Employee> ClassScheduleEmployees { get; set; }
        public List<CertificationFulfillmentStatus> CertificationFulfillmentStatuses { get; set; }
        public int EmergencyResponseCertificationId { get; set; }
        public int ProfHoursCertificationId { get; set; }
        public bool Printforallregisteredstudentsbeforegradeisawarded { get; set; }
        public string NercLogo { get; set; }
        public List<ILA> ILAs { get; set; }
        public ClassCertificatesModel(
            string title,
            string templatePath,
            List<string> displayColumns,
            string companyLogo,
            List<ClientSettings_LabelReplacement> clientSettings_LabelReplacements,
            string defaultTimeZone,
            List<ClassSchedule_Employee> classScheduleEmployees,
            List<CertificationFulfillmentStatus> certificationFulfillmentStatuses,
            int emergencyResponseCertificationId,
            int profHoursCertificationId,
            bool printforallregisteredstudentsbeforegradeisawarded,
            string nercLogo,
            List<ILA> ilas, string dateFormat)
        {
            DisplayColumns = displayColumns;
            TemplatePath = templatePath;
            Title = title;
            CompanyLogo = companyLogo;
            ClientSettings_LabelReplacements = clientSettings_LabelReplacements;
            DefaultTimeZone = defaultTimeZone;
            ClassScheduleEmployees = classScheduleEmployees;
            CertificationFulfillmentStatuses = certificationFulfillmentStatuses;
            EmergencyResponseCertificationId = emergencyResponseCertificationId;
            ProfHoursCertificationId = profHoursCertificationId;
            Printforallregisteredstudentsbeforegradeisawarded = printforallregisteredstudentsbeforegradeisawarded;
            NercLogo = nercLogo;
            ILAs = ilas;
            DateFormat = dateFormat;
        }
    }
}
