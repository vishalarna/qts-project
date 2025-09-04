using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.MetaILA
{
    public class MetaILAILARequirements_VM
    {
        public List<string> Pretests { get; set; }
        public List<string> Tests { get; set; }
        public List<string> CBT { get; set; }

        public MetaILAILARequirements_VM()
        {

        }
        public MetaILAILARequirements_VM(List<string> pretests,List<string> tests,List<string> cbt)
        {
            Pretests = pretests;
            Tests = tests;
            CBT = cbt;
        }
    }
}
