using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ReportFilter : Entity
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public FilterPropertyTypeEnum PropertyType { get; set; }
        public FilterValueTypeEnum ValueType { get; set; }
        public string Value { get; set; }

        public ReportFilter(int reportId, string name, FilterPropertyTypeEnum propertyType, FilterValueTypeEnum valueType, string value)
        {
            ReportId = reportId;
            Name = name;
            PropertyType = propertyType;
            ValueType = valueType;
            UpdateValue(value);
        }

        public void UpdateValue(string value)
        {
            //todo add validation
            Value = value;
        }
    }

    public enum FilterValueTypeEnum
    {
        Single,
        Array,
        Range
    }

    public enum FilterPropertyTypeEnum
    {
        Date,
        Int,
        Boolean,
        String
    }
}
