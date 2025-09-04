using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Report : Entity
    {
        public int ReportSkeletonId { get; set; }
        public int ClientUserId { get; set; }
        public string InternalReportTitle { get; set; }
        public string OfficialReportTitle { get; set; }

        public DateTime? LastRunDate { get; set; }
        public virtual List<ReportFilter> Filters { get; set; }
        public virtual List<ReportDisplayColumn> DisplayColumns { get; set; }

        public Report()
        {

        }
        public Report(int reportSkeletonId, string internalReportTitle, string officialReportTitle)
        {
            ClientUserId = 1;
            ReportSkeletonId = reportSkeletonId;
            InternalReportTitle = internalReportTitle;
            OfficialReportTitle = officialReportTitle;
        }

        public void SetInternalReportTitle(string internalTitle)
        {
            InternalReportTitle = internalTitle;
        }

        public void SetLastRunDate(DateTime lastRunDate)
        {
            LastRunDate = lastRunDate;
        }

        public void UpdateFilter(string filterName, string value)
        {
            if (Filters == null) Filters = new List<ReportFilter>();

            var filter = Filters.Find(e => e.Name == filterName);
            filter.UpdateValue(value);
        }

        public void AddFilter(ReportSkeletonFilter skeletonFilter, string name, string value)
        {
            if (Filters == null) Filters = new List<ReportFilter>();

            var filter = Filters.Where(r => r.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
            if(filter == null)
            {
                Filters.Add(new ReportFilter(this.Id, name, skeletonFilter.PropertyType, skeletonFilter.ValueType, value));
            }
            else
            {
                filter.UpdateValue(value);
            }
        }

        public void ClearFilter(string filterName)
        {
            if (Filters == null) Filters = new List<ReportFilter>();

            var filter = Filters.Find(d => d.Name == filterName);
            if(filter != null)
            {
                Filters.Remove(filter);
            }
        }

        public void EnableDisplayColumn(string columnName)
        {
            if (DisplayColumns == null) DisplayColumns = new List<ReportDisplayColumn>();

            var column = DisplayColumns.Find(d => d.ColumnName == columnName);
            if(column != null)
            {
                column.DoDisplay();             
            }
            else if(columnName != "")
            {
                var displayColumn = new ReportDisplayColumn(columnName, true);
                DisplayColumns.Add(displayColumn);
            }
        }
        
        public void DisableDisplayColumn(string columnName)
        {
            if (DisplayColumns == null) DisplayColumns = new List<ReportDisplayColumn>();

            var column = DisplayColumns.Find(d => d.ColumnName == columnName);
            if(column != null)
            {
                column.DontDisplay();        
            }
            else
            {
                var displayColumn = new ReportDisplayColumn(columnName, false);
                DisplayColumns.Add(displayColumn);

            }
        }
    }
}
