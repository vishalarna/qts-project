using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.TrainingIssue
{
   public class TrainingIssue_DataElementCategory_VM
    {
        public string Name { get; set; }        
        public List<TrainingIssue_DataElement_VM> DataElementVMs { get; set; } = new List<TrainingIssue_DataElement_VM>();
        public TrainingIssue_DataElementCategory_VM()
        {

        }
        public TrainingIssue_DataElementCategory_VM(string name)
        {
            Name = name;
        }
    }
}
