using QTD2.Infrastructure.Model;
using QTD2.Infrastructure.Model.Task_List_Review;
using QTD2.Infrastructure.Model.TrainingIssue;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QTD2.Infrastructure.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonSerializer.Serialize(self);
            return JsonSerializer.Deserialize<T>(serialized);
        }
        public static DateTime ConvertLocalTimeToUTCTime(this DateTime localtime, string timeZoneId)
        {
            try
            {
                localtime = new DateTime(localtime.Year, localtime.Month,
            localtime.Day, localtime.Hour, localtime.Minute,
            localtime.Second, DateTimeKind.Unspecified);

                TimeZoneInfo TheTZ = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                TimeSpan TheOffset = TheTZ.GetUtcOffset(localtime);

                DateTimeOffset TheUTCTimeOffset = new DateTimeOffset(
                    localtime, TheOffset).ToUniversalTime();

                DateTime TheUTCTime = new DateTime(TheUTCTimeOffset.Year,
                    TheUTCTimeOffset.Month, TheUTCTimeOffset.Day, TheUTCTimeOffset.Hour,
                    TheUTCTimeOffset.Minute, 0, DateTimeKind.Utc);

                return TheUTCTime;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public static string CompressImage(this string inputImage)
        {
            var items = inputImage.Split(',');
            if (!(items.Length > 0))
            {
                return inputImage;
            }
            byte[] inputBytes = Convert.FromBase64String(items[1]);
            int targetWidth = 200;
            int targetHeight = 200;
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (Image originalImage = Image.FromStream(inputStream))
            {
                int originalWidth = originalImage.Width;
                int originalHeight = originalImage.Height;

                if (originalWidth <= 200 && originalHeight <= 200)
                {
                    return inputImage;
                }


                // Calculate the new dimensions while maintaining the aspect ratio
                double aspectRatio = (double)originalWidth / originalHeight;
                int newWidth, newHeight;
                if (originalWidth > originalHeight)
                {
                    newWidth = targetWidth;
                    newHeight = (int)(targetWidth / aspectRatio);
                }
                else
                {
                    newHeight = targetHeight;
                    newWidth = (int)(targetHeight * aspectRatio);
                }

                using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
                using (Graphics graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        resizedImage.Save(outputStream, ImageFormat.Jpeg); // You can use other formats as needed
                        byte[] compressedBytes = outputStream.ToArray();

                        string compressedBase64 = Convert.ToBase64String(compressedBytes);
                        return "data:image/png;base64," + compressedBase64;
                    }
                }
            }
        }

        public static DateTime ConvertUTCTimeToLocalTime(this DateTime TheUTCTime, string TheTimezoneID)
        {
            TimeZoneInfo TheTZ = TimeZoneInfo.FindSystemTimeZoneById(TheTimezoneID);

            DateTime UTCTime = new DateTime(TheUTCTime.Year, TheUTCTime.Month,
               TheUTCTime.Day, TheUTCTime.Hour, TheUTCTime.Minute, 0, DateTimeKind.Utc);

            DateTime TheUserTime = TimeZoneInfo.ConvertTime(UTCTime, TheTZ);

            return TheUserTime;
        }

        public static DateTime ConvertToDefaultTimeZone(this DateTime theTime, string theTimezone)
        {
            var timezoneModel = GetTimezones().FirstOrDefault(x => x.Timezone == (theTimezone ?? "Central Standard Time"));
            TimeZoneInfo TheTZ = TimeZoneInfo.FindSystemTimeZoneById(timezoneModel.Timezone);

            DateTime UTCTime = theTime.Kind == DateTimeKind.Utc ? theTime : (theTime.Kind == DateTimeKind.Unspecified ? new DateTime(theTime.Year, theTime.Month, theTime.Day, theTime.Hour, theTime.Minute, 0, DateTimeKind.Utc) : theTime.ToUniversalTime());

            DateTime TheUserTime = TimeZoneInfo.ConvertTime(UTCTime, TheTZ);

            return TheUserTime;
        }

        // This method is being used in Reports CSHTML files
        public static string ConvertWithTimeZoneName(this DateTime theTime, string theTimezone)
        {
            var timezoneModel = GetTimezones().FirstOrDefault(x => x.Timezone == (theTimezone ?? "Central Standard Time"));
            theTime = ConvertToDefaultTimeZone(theTime, theTimezone);
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneModel.Timezone);
            string abbreviation = timezone.IsDaylightSavingTime(theTime) ? timezoneModel.DaylightAbbreviation : timezoneModel.StandardAbbreviation;
            return $"{theTime.ToString("MM/dd/yyyy hh:mm tt")} {abbreviation}";
        }

        public static DateTime ToReportDate(this DateTime theTime, string timezone)
        {
            return theTime.ConvertToDefaultTimeZone(timezone);
        }

        public static DateTime ToReportDate(this DateTime theTime, bool convert, string timezone)
        {
            return convert ? theTime.ToReportDate(timezone) : theTime;
        }

        public static DateTime ConvertFromDefaultTimeZone(this DateTime theTime, string theTimezone)
        {
            var timezoneModel = GetTimezones().FirstOrDefault(x => x.Timezone == (theTimezone ?? "Central Standard Time"));
            TimeZoneInfo theTZ = TimeZoneInfo.FindSystemTimeZoneById(timezoneModel.Timezone);
            DateTime UTCTime = TimeZoneInfo.ConvertTimeToUtc(theTime, theTZ);
            return UTCTime;
        }

        public static List<TimezoneModel> GetTimezones()
        {
            return new List<TimezoneModel>
            {
                new TimezoneModel("Eastern Standard Time", "EST", "EDT"),
                new TimezoneModel("Central Standard Time", "CST", "CDT"),
                new TimezoneModel("Mountain Standard Time", "MST", "MDT"),
                new TimezoneModel("US Mountain Standard Time", "MST", "MST"),
                new TimezoneModel("Pacific Standard Time", "PST", "PDT"),
                new TimezoneModel("Alaskan Standard Time", "AKST", "AKDT"),
                new TimezoneModel("Hawaiian Standard Time", "HAST", "HADT"),
                new TimezoneModel("Atlantic Standard Time", "AST", "ADT"),
                new TimezoneModel("Newfoundland Standard Time", "NST", "NDT")
            };
        }

        public static List<Task_Review_ActionType_OperationTypeModel> GetOperationTypeByActionType()
        {
            return new List<Task_Review_ActionType_OperationTypeModel>()
            {
                new Task_Review_ActionType_OperationTypeModel("DutyArea",""),
                new Task_Review_ActionType_OperationTypeModel("SubDutyArea","ActionItem_SubDuty_Operation"),
                new Task_Review_ActionType_OperationTypeModel("MetaTask",""),
                new Task_Review_ActionType_OperationTypeModel("Task",""),
                new Task_Review_ActionType_OperationTypeModel("Steps","ActionItem_Step_Operation"),
                new Task_Review_ActionType_OperationTypeModel("QuestionAndAnswer","ActionItem_QuestionAndAnswer_Operation"),
                new Task_Review_ActionType_OperationTypeModel("TaskSpecificSuggestions","ActionItem_Suggestion_Operation"),
                new Task_Review_ActionType_OperationTypeModel("Conditions",""),
                new Task_Review_ActionType_OperationTypeModel("Criteria",""),
                new Task_Review_ActionType_OperationTypeModel("References",""),
                new Task_Review_ActionType_OperationTypeModel("Tools","ActionItem_Tool_Operation"),
                new Task_Review_ActionType_OperationTypeModel("EnablingObjective","ActionItem_EnablingObjective_Operation"),
                new Task_Review_ActionType_OperationTypeModel("Procedure","ActionItem_Procedure_Operation"),
                new Task_Review_ActionType_OperationTypeModel("RegulatoryRequirements","ActionItem_RegulatoryRequirement_Operation"),
                new Task_Review_ActionType_OperationTypeModel("SafetyHazards","ActionItem_SafetyHazard_Operation"),
                new Task_Review_ActionType_OperationTypeModel("Other",""),
                new Task_Review_ActionType_OperationTypeModel("PrepareForTaskRequalification","PrepareForTaskRequalification_ActionItem"),
                new Task_Review_ActionType_OperationTypeModel("MakeTaskInactive","MakeTaskInactive_ActionItem")
            };
        }

        public static List<TrainingIssue_DataElementCategory_VM> GetAllDataElementsWithCategories()
        {
            return new List<TrainingIssue_DataElementCategory_VM>
            {
                new TrainingIssue_DataElementCategory_VM("My Data")
                {
                    DataElementVMs = new List<TrainingIssue_DataElement_VM>
                    {
                        new TrainingIssue_DataElement_VM("Enabling Objective", "EnablingObjective","My Data"),
                        new TrainingIssue_DataElement_VM("Meta Enabling Objective", "MetaEnablingObjective","My Data"),
                        new TrainingIssue_DataElement_VM("Meta Task", "MetaTask","My Data"),
                        new TrainingIssue_DataElement_VM("Procedure", "Procedure","My Data"),
                        new TrainingIssue_DataElement_VM("Regulatory Requirement", "RegulatoryRequirement","My Data"),
                        new TrainingIssue_DataElement_VM("Safety Hazard", "SafetyHazard","My Data"),
                        new TrainingIssue_DataElement_VM("Task", "Task","My Data"),
                        new TrainingIssue_DataElement_VM("Tool", "Tool","My Data"),
                    }
                },
                new TrainingIssue_DataElementCategory_VM("Training Material Design")
                {
                    DataElementVMs = new List<TrainingIssue_DataElement_VM>
                    {
                        new TrainingIssue_DataElement_VM("Training Program", "TrainingProgram","Training Material Design"),
                        new TrainingIssue_DataElement_VM("ILAs/Courses", "ILAsCourses","Training Material Design"),
                        new TrainingIssue_DataElement_VM("MetaILAs/Courses", "MetaILAsCourses","Training Material Design"),
                    }
                },
                new TrainingIssue_DataElementCategory_VM("Training Material Development")
                {
                    DataElementVMs = new List<TrainingIssue_DataElement_VM>
                    {
                        new TrainingIssue_DataElement_VM("Computer Based Training", "ComputerBasedTraining","Training Material Development"),
                        new TrainingIssue_DataElement_VM("Test Item", "TestItem","Training Material Development"),
                        new TrainingIssue_DataElement_VM("Pretest", "Pretest","Training Material Development"),
                        new TrainingIssue_DataElement_VM("Test", "Test","Training Material Development"),
                    }
                }
            };
        }

        public static int[] RandomizeArray(int[] input)
        {
            var availableNumbers = input.ToList();
            var random = new Random();
            var result = new List<int>();

            while (availableNumbers.Count > 0)
            {
                int index = random.Next(availableNumbers.Count);
                result.Add(availableNumbers[index]);
                availableNumbers.RemoveAt(index);
            }
            return result.ToArray();
        }

    }
}
