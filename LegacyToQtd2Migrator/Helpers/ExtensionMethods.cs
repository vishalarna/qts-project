using LegacyToQtd2Migrator.Vision.Data;
using RtfPipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Xml;

namespace LegacyToQtd2Migrator.Helpers
{
    public static class ExtensionMethods
    {
        public static string InstanceTimeZone = "Central Standard Time";

        public static DateTime? ToQtsTime(this DateTime? dateTime, bool isDateOnly)
        {
            if (!dateTime.HasValue) return dateTime;

            var _dateTime = dateTime.Value;

            return calculateTime(_dateTime, isDateOnly);
        }

        //public static DateTime ToQtsTime(this DateTime? dateTime, bool isDateOnly)
        //{
        //    var hourOffset = HourDifferenceWithLocal;
        //    var _dateTime = dateTime.HasValue ? dateTime.Value : DateTime.MinValue;

        //    return calculateTime(_dateTime, hourOffset, isDateOnly);
        //}

        public static DateTime ToQtsTime(this DateTime dateTime, bool isDateOnly)
        {

            var _dateTime = dateTime;

            return calculateTime(_dateTime, isDateOnly);
        }

        private static DateTime calculateTime(DateTime dateTime, bool isDateOnly)
        {
            if (isDateOnly)
            {
                // Set dateTime to Noon if its a date only, don't shift to UTC either
                //This should only be used when we are in the process of changing a migrated property to Date only, yet still want to set the value in a meaningful way
                //Eventually no fields should be using this as we should just convert in the Map to Date only
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0);
            }

            var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(InstanceTimeZone);

            if (dateTime.Kind == DateTimeKind.Local)
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            }

            return TimeZoneInfo.ConvertTimeToUtc(dateTime, targetTimeZone);
        }

        public static List<AnalysisImpl> GetVisionAnalysisImps(this VisionContext context, int projectId, string entity, List<Func<AnalysisImpl, bool>> additionalPredicates)
        {
            var query = context
                    .AnalysisImpls
                    .Where(r => r.FkProject == projectId)
                    .Where(r => !r.FkExpiredBy.HasValue)
                    .Where(r => r.FkAnalysisLevelNavigation.AnalysisLevelImpls.First().Text == entity);

            if (additionalPredicates != null)
            {
                foreach (var predicate in additionalPredicates)
                {
                    query.Where(predicate);
                }
            }


            return query.ToList();

        }

        public static List<ObjectiveImpl> GetVisionObjectiveImps(this VisionContext context, int projectId, string entity, List<Func<ObjectiveImpl, bool>> additionalPredicates)
        {
            var query = context
                    .ObjectiveImpls
                    .Where(r => r.FkProject == projectId)
                    .Where(r => !r.FkExpiredBy.HasValue)
                    .Where(r => r.FkObjectiveLevelNavigation.ObjectiveLevelImpls.First().Text == entity)
                    .AsQueryable();

            if (additionalPredicates != null)
            {
                foreach (var predicate in additionalPredicates)
                {
                    query = query.Where(predicate).AsQueryable();
                }
            }


            return query.ToList();

        }


        public static string CombineRtfStrings(this List<string> rtfStrings)
        {
            return string.Join("\n", rtfStrings.Select(rtf => HtmlToPlainText(Rtf.ToHtml(rtf))));
        }

        public static string RtfToHtml(this string rtf)
        {
            return Rtf.ToHtml(rtf)?.Trim();
        }

        public static string RtfToPlainText(this string rtf)
        {
            return HtmlToPlainText(Rtf.ToHtml(rtf))?.Trim();
        }

        public static TaskParsingDTO ParseVisionTaskText(this string input, AnalysisImpl impl)
        {
            string pattern = @"^(\d+)\.(\d+)\.(\d+)\s+(.+)$";

            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string part1 = match.Groups[1].Value;
                string part2 = match.Groups[2].Value;
                string part3 = match.Groups[3].Value;
                string anyText = match.Groups[4].Value;

                return new TaskParsingDTO(match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value, impl.Id);
            }
            else
            {
                return new TaskParsingDTO(input, impl.Id);
            }
        }

        public static DutyAreaParsingDTO ParseVisionDutyAreaText(this string input, AnalysisImpl impl)
        {
            var taskParsing = input.ParseVisionTaskText(impl);

            if(taskParsing.TaskNumberFound)
            {
                return new DutyAreaParsingDTO(taskParsing.DutyAreaNumber);
            }
            else
            {
                return new DutyAreaParsingDTO();
            }
        }

        public static string GetIlaNumber(this string input, string fallback)
        {
            Match match = Regex.Match(input, @"^[A-Za-z]+_\d+_(\d+)");
            if (match.Success)
            {
                string extracted = match.Groups[1].Value;
                return extracted;
            }

            return fallback;
        }

        private static string HtmlToPlainText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }

        public static string SanitizeXml(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var buffer = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                if (XmlConvert.IsXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }
    }
}
