using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Import_CSV_VM
{
    public class ImportDatum_ILA_VM
    {
        public string IlaName { get; set; }
        public string IlaNum { get; set; }
        public string IlaDesc { get; set; }
        public string SelfPaced { get; set; }
        public string TotalHours { get; set; }
        public string ProviderName { get; set; }
        public string DeliveryMethodName { get; set; }
        public string EffectiveDate { get; set; }
        public string NercIsIncludeSimulation { get; set; }
        public string NercEmergencyOperatingTraining { get; set; }
        public string NercIsPartialCred { get; set; }
        public string NercTotalCEH { get; set; }
        public string NercStandards { get; set; }
        public string NercSimulation { get; set; }
        public string Reg { get; set; }
        public string Reg2 { get; set; }
        public List<ValidationError_VM> ValidationErrors { get; set; }
    }

}
