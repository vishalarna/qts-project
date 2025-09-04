using QTD2.Infrastructure.Model.ILA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Provider
{
    public class ILAProviderDataVM
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public bool ProviderActive { get; set; }
        public List<ILADetailsVM> ILADetails = new List<ILADetailsVM>();
        public ILAProviderDataVM() { }
        public ILAProviderDataVM(int providerId, string providerName,bool providerActive ,List<ILADetailsVM> iLADetails)
        {
            ProviderId = providerId;
            ProviderName = providerName;
            ILADetails = iLADetails;
            ProviderActive = providerActive;
        }
    }
}
