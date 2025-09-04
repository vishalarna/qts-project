using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ReportDisplayColumn : Entity
    {
        public int ReportId { get; set; }
        public string ColumnName { get; set; }
        public bool Display { get; set; }

        public ReportDisplayColumn(string columnName, bool display)
        {
            ColumnName = columnName;
            Display = display;
        }

        public void DoDisplay()
        {
            Display = true;
        }

        public void DontDisplay()
        {
            Display = false;
        }
    }
}
