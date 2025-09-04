using QTD2.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public interface ILicenseInterpreter
    {
        string ConvertActivationCodeToAccessCode();
        string ConvertAccessCodeToActivationCode(string accessCode);
        public string GetVersion();
        public bool GetHasEMP();
        public bool GetHasTDT();
        public DateTime GetExpiration();
        public bool GetCrbAccess();
        public string GetLicenseType();
        public int GetMaxEmployees();
    }

    //''' The access code entered by the client is parsed and decoded.  Properties are set
    //''' which are retrieved by the calling function.
    //''' Level, Access, CRB (1 digit), Expiration Date (6 digits - MMDDYY)
    //''' Total 10 digits - see remarks for composition of code.
    //''' Once code is determined, it is encoded:
    //''' (Code + 5) * (Client ID + 15) = encoded activation code.
    //''' To decode: 
    //''' [(Activation Code) / (Client ID + 15)] - 5
    //''' </summary>
    //''' <remarks>
    //''' First digit---------------
    //'''  Level 1: up to 9 Employee records
    //'''	 Level 2: 10 to 19 Employee records
    //'''	 Level 3: 20 to 49 Employee records
    //'''	 Level 4: 50 or more records
    //''' Second digit--------------
    //'''  Case 0 'full access
    //'''  Case 1  'Training Design only
    //'''  Case 2  'Scheduling and Tracking only
    //'''  Case 4  'DELUXE version    'CAJ 8/21/14
    //''' Third digit --------------
    //'''  1/0 CRB Access
    //''' Fourth-Ninth digits-------
    //'''  expiration date mmddyy
    //''' 10th digit
    //'''  test development tool 
    //''' 11th digit
    //'''  employee management portal
    //''' 12th digit
    //'''  spare...
    //''' tenth digit---------------
    //'''  revised 4/26/09 (Sarah)
    //'''  (user ID+15) * (your concatenation -5)
    //''' 13th -14th digit
    //''' version

    public class LicenseInterpreter : ILicenseInterpreter
    {
        int _clientId;
        string _activationCode;
        string _accessCode;

        public LicenseInterpreter(int clientId)
        {
            _clientId = clientId;
        }

        public LicenseInterpreter(int clientId, string activationCode)
        {
            _clientId = clientId;
            _activationCode = activationCode;
            _accessCode = ConvertActivationCodeToAccessCode();
        }

        public string ConvertAccessCodeToActivationCode(string accessCode)
        {
            _accessCode = accessCode;

            long accessCodeLong;
            bool accessCodeIsNumber = long.TryParse(_accessCode, out accessCodeLong);

            if (!accessCodeIsNumber)
                throw new ArgumentException("Access Code is not valid");

            long activationCode = (accessCodeLong + 5) * (_clientId + 15);

            return activationCode.ToString();
        }

        public string ConvertActivationCodeToAccessCode()
        {
            long activationCodeLong;
            bool activationCodeIsNumber = long.TryParse(_activationCode, out activationCodeLong);

            if (!activationCodeIsNumber)
                throw new ArgumentException("Access Code is not valid");

            long accessCode = (activationCodeLong / (_clientId + 15)) - 5;

            return accessCode.ToString();
        }

        public string GetVersion()
        {
            return _accessCode.Substring(12, 2);
        }

        public bool GetHasEMP()
        {
            string has = _accessCode.Substring(10, 1);
            int hasInt = Convert.ToInt32(has);

            if (hasInt < 0 || hasInt > 2)
                throw new QTDServerException("Value of has emp out of range");

            return hasInt >= 1;
        }

        public bool GetHasTDT()
        {
            string has = _accessCode.Substring(9, 1);
            int hasInt = Convert.ToInt32(has);

            if (hasInt != 0 && hasInt != 1)
                throw new QTDServerException("Value of has tdt must be boolean");

            return Convert.ToBoolean(hasInt);
        }

        public DateTime GetExpiration()
        {
            string month = _accessCode.Substring(3, 2);
            string day = _accessCode.Substring(5, 2);
            string year = _accessCode.Substring(7, 2);

            int monthInt;
            int dayInt;
            int yearInt;

            bool isMonthInt = int.TryParse(month, out monthInt);
            bool isDayInt = int.TryParse(day, out dayInt);
            bool isYearInt = int.TryParse(year, out yearInt);

            if (!isMonthInt || monthInt < 1 || monthInt > 12)
                throw new ArgumentException("Invalid Month in Access Code");

            if (!isDayInt || dayInt < 1 || dayInt > 31)
                throw new ArgumentException("Invalid Day in Access Code");

            if (!isYearInt || yearInt < 11 || yearInt > 150)
                throw new ArgumentException("Invalid Year in Access Code");

            return new DateTime(yearInt + 2000, monthInt, dayInt);
        }

        public bool GetCrbAccess()
        {
            return Convert.ToBoolean(_accessCode.Substring(2, 1));
        }

        public string GetLicenseType()
        {
            int level = Convert.ToInt32(_accessCode.Substring(1, 1));

            switch (level)
            {
                case 0:
                    return "Basic Access";
                case 1:
                    return "Full Access";
                case 2:
                    return "Training Design Only";
                case 3:
                    return "Scheduling and Tracking Only";
                case 4:
                    return "Deluxe Version";
                case 5:
                    return "Core";
                default:
                    throw new ArgumentException("Invalid Version Level");
            }
        }

        public int GetMaxEmployees()
        {
            int level = Convert.ToInt32(_accessCode.Substring(0, 1));

            switch (level)
            {
                case 1:
                    return 9;
                case 2:
                    return 19;
                case 3:
                    return 49;
                case 4:
                    return 99;
                case 5:
                    return 249;
                case 6:
                    return 999;
                case 7:
                    return 2499;
                case 8:
                    return 100000;
                default:
                    return 0;

            }
        }
    }
}
