using LegacyToQtd2Migrator.QTD2ScormContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Helpers
{
    public class LicenseConverter
    {
        public QTD2.Domain.Entities.Core.ClientSettings_License ConvertLegacyLicense(string activationCode, string clientId)
        {
            double activationCodeLong = Convert.ToDouble(activationCode);
            double clientIdLong = Convert.ToDouble(clientId);

            double accessCode = (activationCodeLong / (clientIdLong + 15)) - 5;

            string accessCodeString = accessCode.ToString();

            string newAccessCodeString = accessCodeString + "01";

            int clientIdInt = Convert.ToInt32(clientIdLong);

            QTD2.Domain.Entities.Core.LicenseInterpreter licenseInterpreter = new QTD2.Domain.Entities.Core.LicenseInterpreter(clientIdInt);
            var newActivationCode = licenseInterpreter.ConvertAccessCodeToActivationCode(newAccessCodeString);

            return new QTD2.Domain.Entities.Core.ClientSettings_License(clientIdInt, newActivationCode, null);
        }
    }
}
