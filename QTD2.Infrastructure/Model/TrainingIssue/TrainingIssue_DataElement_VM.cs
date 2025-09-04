using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
    public class TrainingIssue_DataElement_VM
    {
        public int? Id { get; set; }
        public string DataElementType { get; set; }
        public int? DataElementId { get; set; }
        public string DataElementDisplay { get; set; }
        public string DataElementDescription { get; set; }
        public string DataElementCategory { get; set; }

        public TrainingIssue_DataElement_VM(int? id, string dataElementType, int? dataElementId, string dataElementDisplay, string dataElementDescription, string dataElementCategory)
        {
            Id = id;
            DataElementType = dataElementType;
            DataElementId = dataElementId;
            DataElementDisplay = dataElementDisplay;
            DataElementDescription = dataElementDescription;
            DataElementCategory = dataElementCategory;
        }

        public TrainingIssue_DataElement_VM(string dataElementDisplay, string dataElementType, string dataElementCategory)
        {
            DataElementDisplay = dataElementDisplay;
            DataElementType = dataElementType;
            DataElementCategory = dataElementCategory;
        }

        public TrainingIssue_DataElement_VM()
        {
        }
    }
}
