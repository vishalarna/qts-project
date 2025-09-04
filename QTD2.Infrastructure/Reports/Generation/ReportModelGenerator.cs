using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Reports.Generation.Models;
using QTD2.Infrastructure.Reports.Interfaces;
using QTD2.Domain.Exceptions;

namespace QTD2.Infrastructure.Reports.Generation
{
    public class ReportModelGenerator : IReportModelGenerator
    {
        public virtual Task<IReportModel> GenerateModel(Report report)
        {
            throw new NotImplementedException();
        }

		protected T ExtractParameters<T>(List<ReportFilter> filters, string filterName)
		{
			var filter = filters.Where(r => r.Name.ToUpper() == filterName.ToUpper()).FirstOrDefault();

			if (filter == null)
				throw new QTDServerException(filterName + " should not be empty",false);

			string parameterString = filter.Value;

			if (String.IsNullOrEmpty(parameterString))
				throw new QTDServerException(filterName + " should not be empty",false);

            switch (typeof(T))
            {
                case Type listDates when listDates == typeof(List<DateTime>):
                    return (T)(object)parseDateRange(parameterString);
                case Type listInts when listInts == typeof(List<int>):
                    return (T)(object)parseIntList(parameterString);
                case Type listStrings when listStrings == typeof(List<string>):
                    return (T)(object)parseStringList(parameterString);
                case Type listBooleans when listBooleans == typeof(List<bool>):
                    return (T)(object)parseBoolList(parameterString);
                case Type str when str == typeof(string):
                    return (T)(object)parseString(parameterString);
                case Type integer when integer == typeof(int):
                    return (T)(object)parseInt(parameterString);
                case Type boolean when boolean == typeof(bool):
                    return (T)(object)parseBool(parameterString);
                case Type date when date == typeof(DateTime):
                    return (T)(object)parseDate(parameterString);
                default:
                    throw new NotImplementedException();
            }
        }
        private List<DateTime> parseDateRange(string parameterString)
        {
            var dates = parameterString.Split(",");

            var date0String = dates.ElementAtOrDefault(0);
            var date1String = dates.ElementAtOrDefault(1);

            if (String.IsNullOrEmpty(date0String))
                throw new QTDServerException("The Start Date is empty",false);

            if (String.IsNullOrEmpty(date1String))
                throw new QTDServerException("The End Date is empty", false);

            DateTime date0;
            DateTime date1;

            bool date0Valid = DateTime.TryParse(date0String, out date0);
            bool date1Valid = DateTime.TryParse(date1String, out date1);

            if (!date0Valid)
                throw new QTDServerException("The Start Date is in an invalid format", false);

            if (!date1Valid)
                throw new QTDServerException("The End Date is in an invalid format", false);

            return new List<DateTime>()
            {
                date0,
                date1
            };
        }

        private List<int> parseIntList(string parameterString)
        {
            var integerStrings = parameterString.Split(",");
            if (integerStrings.Length == 0)
                throw new QTDServerException("The input parameter string does not contain any values", false);
            var integers = new List<int>();
            foreach (var integerString in integerStrings)
            {
                if (!int.TryParse(integerString, out int parsedInt))
                    throw new QTDServerException($"Invalid integer format: {integerString}", false);
                integers.Add(parsedInt);
            }
            return integers;
        }

        private List<string> parseStringList(string parameterString)
        {
            var stringValues = parameterString.Split(",");
            if (stringValues.Length == 0)
                throw new QTDServerException("The input parameter string does not contain any values", false);
            var strings = new List<string>();
            foreach (var stringValue in stringValues)
            {
                if (string.IsNullOrEmpty(stringValue))
                    throw new QTDServerException("One or more values in input string is null or empty", false);
                strings.Add(stringValue);
            }
            return strings;
        }

        private List<bool> parseBoolList(string parameterString)
        {
            var boolStrings = parameterString.Split(",");
            if (boolStrings.Length == 0)
                throw new QTDServerException("The input parameter string does not contain any values", false);
            var boolList = new List<bool>();
            foreach (var boolString in boolStrings)
            {
                if (!bool.TryParse(boolString, out bool parsedBool))
                    throw new QTDServerException($"Invalid boolean format: {boolString}", false);

                boolList.Add(parsedBool);
            }
            return boolList;
        }

        private int parseInt(string parameterString)
        {
            if (!int.TryParse(parameterString, out int parsedInt))
                throw new QTDServerException($"Invalid integer format: {parameterString}", false);
            return parsedInt;
        }

        private string parseString(string parameterString)
        {
            if (string.IsNullOrEmpty(parameterString))
                throw new QTDServerException("The input string is null or empty", false);
            return parameterString;
        }

        private bool parseBool(string parameterString)
        {
            if (!bool.TryParse(parameterString, out bool parsedBool))
                throw new QTDServerException($"Invalid boolean format: {parameterString}", false);
            return parsedBool;
        }

        private DateTime parseDate(string parameterString)
        {
            if (string.IsNullOrEmpty(parameterString))
                throw new QTDServerException("The input date is null or empty", false);
            if (!DateTime.TryParse(parameterString, out DateTime parsedDate))
                throw new QTDServerException($"Invalid date format: {parameterString}", false);
            return parsedDate;
        }
    }
}
