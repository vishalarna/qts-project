using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Helpers
{
    public static class NotificationDateRangeHelper
    {
        public static List<DateTime> CalculatePriorDateRange(string emailFrequency, int hours, int minutes, DayOfWeek dayOfWeek, int dayOfMonth, string defaultTimeZone)
        {
            TimeZoneInfo instanceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(defaultTimeZone);
            DateTime currentInstanceDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, instanceTimeZone);

            DateTime tempInstanceDateTime = new DateTime(currentInstanceDateTime.Year, currentInstanceDateTime.Month, currentInstanceDateTime.Day, hours, minutes, 0);

            DateTime endUTCDateTime = new DateTime();
            DateTime startUTCDateTime = new DateTime();

            switch (emailFrequency)
            {
                case "Daily":
                    if (tempInstanceDateTime > currentInstanceDateTime)
                    {
                        tempInstanceDateTime = tempInstanceDateTime.AddDays(-1);
                    }
                    endUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime, instanceTimeZone);
                    startUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime.AddDays(-1), instanceTimeZone); // Don't get smart and try to set this to endDateTime.AddDays(-1), if the prior day was when DLST triggered, you want it to consider that hour difference, let Microsoft handle it
                    break;
                case "Weekly":
                    var dayOfWeek_index_offset = (int)currentInstanceDateTime.DayOfWeek;
                    if (dayOfWeek_index_offset < (int)dayOfWeek)
                    {
                        dayOfWeek_index_offset += 7;
                    }
                    tempInstanceDateTime = tempInstanceDateTime.AddDays(-(dayOfWeek_index_offset - (int)dayOfWeek));
                    if (tempInstanceDateTime > currentInstanceDateTime)
                    {
                        tempInstanceDateTime = tempInstanceDateTime.AddDays(-7);
                    }
                    endUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime, instanceTimeZone);
                    startUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime.AddDays(-7), instanceTimeZone);  // Don't get smart and try to set this to endDateTime.AddDays(-7), if the prior week was when DLST triggered, you want it to consider that hour difference, let Microsoft handle it
                    break;
                case "Monthly":
                    DateTime dummyDateTime;
                    int dummyDateTime_DaysInMonth;

                    var tempInstanceDateTime_DaysInMonth = DateTime.DaysInMonth(tempInstanceDateTime.Year, tempInstanceDateTime.Month);
                    var tempInstanceDateTime_DayOfMonth = new DateTime(tempInstanceDateTime.Year, tempInstanceDateTime.Month, int.Min(dayOfMonth, tempInstanceDateTime_DaysInMonth), tempInstanceDateTime.Hour, tempInstanceDateTime.Minute, 0);

                    var daysBetween = (tempInstanceDateTime - tempInstanceDateTime_DayOfMonth).Days;
                    if (daysBetween > 0)
                    {
                        //Shift tempInstanceDateTime back the difference in days
                        tempInstanceDateTime = tempInstanceDateTime.AddDays(-daysBetween);
                    }
                    else if (daysBetween < 0 || tempInstanceDateTime > currentInstanceDateTime)
                    {
                        // Calculate to the prior month, calculate it's DayOfMonth based on it's DaysInMonth, then set tempInstanceDateTime based on that date
                        dummyDateTime = tempInstanceDateTime;
                        dummyDateTime = dummyDateTime.AddMonths(-1);
                        dummyDateTime_DaysInMonth = DateTime.DaysInMonth(dummyDateTime.Year, dummyDateTime.Month);

                        tempInstanceDateTime = new DateTime(dummyDateTime.Year, dummyDateTime.Month, int.Min(dayOfMonth, dummyDateTime_DaysInMonth), tempInstanceDateTime.Hour, tempInstanceDateTime.Minute, 0);
                    }
                    endUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime, instanceTimeZone);
                    //To get the appropriate startDate, we need to similarly use a dummy date to get back 1 month, see how many days are in that month, and then compensate appropriately
                    // You can't just endDateTime.AddMonth(-1)
                    // If you did, then you'd have the following type of problems:
                    //  Monthly Email, sent on the 30th each month, January sent for 12/30 to 1/30
                    //  It's 2/28 today, the above logic chooses 2/28 as the endDateTime, if we only did endDateTime.AddMonth(-1)
                    //  you'd get 1/28 and then be including dates that were already processed of 1/28, 1/29, and 1/30
                    // in addition to a DLST problem (like the other two scenarios mentioned above)

                    dummyDateTime = tempInstanceDateTime;
                    dummyDateTime = dummyDateTime.AddMonths(-1);
                    dummyDateTime_DaysInMonth = DateTime.DaysInMonth(dummyDateTime.Year, dummyDateTime.Month);

                    tempInstanceDateTime = new DateTime(dummyDateTime.Year, dummyDateTime.Month, int.Min(dayOfMonth, dummyDateTime_DaysInMonth), tempInstanceDateTime.Hour, tempInstanceDateTime.Minute, 0);

                    startUTCDateTime = TimeZoneInfo.ConvertTimeToUtc(tempInstanceDateTime, instanceTimeZone);
                    break;
            }

            return new List<DateTime>() { startUTCDateTime, endUTCDateTime };
        }
    }
}
