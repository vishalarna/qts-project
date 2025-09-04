using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ClientSettings_GeneralSettings : Entity
    {
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string DateFormat { get; set; }
        public string ClassStartEndTimeFormat { get; set; }
        public decimal CompanySpecificCoursePassingScore { get; set; }
        public string DefaultTimeZone { get; set; }

        public ClientSettings_GeneralSettings()
        {

        }

        public ClientSettings_GeneralSettings(string companyName, string companyLogo, string dateFormat, string classStartEndTimeFormat, decimal companySpecificCoursePassingScore)
        {
            CompanyName = companyName;
            CompanyLogo = companyLogo;
            DateFormat = dateFormat;
            ClassStartEndTimeFormat = classStartEndTimeFormat;
            CompanySpecificCoursePassingScore = companySpecificCoursePassingScore;
        }


        public void SetCompanyName(string companyName)
        {
            this.CompanyName = companyName;
        }

        public void SetCompanyLogo(string companyLogo)
        {
            //TODO validate that this is an image

            this.CompanyLogo = companyLogo;
        }

        public void SetDateFormat(string dateFormat)
        {
            //todo validate against a list of acceptable formats
            this.DateFormat = dateFormat;
        }

        public void SetDefaultTimeZone(string timeZone)
        {
            //todo validate against a list of acceptable formats
            this.DefaultTimeZone = timeZone;
        }

        public void SetClassStartEndTimeFormat(string classStartAndEndTimeFormat)
        {
            //todo validate against a list of acceptable formats
            this.ClassStartEndTimeFormat = classStartAndEndTimeFormat;
        }
    }

}
