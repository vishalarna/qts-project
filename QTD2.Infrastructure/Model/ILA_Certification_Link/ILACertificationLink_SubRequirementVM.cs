using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Certification_Link
{
    public class ILACertificationLink_SubRequirementVM
    {
        public int ILACertificationLinkId { get; set; }
        public double? CEHHours { get; set; }
        public List<ILACertificationSubRequirementLinkVM> ILACertificationSubRequirementLinkVM = new List<ILACertificationSubRequirementLinkVM>();
        public ILACertificationLink_SubRequirementVM() { }
        public ILACertificationLink_SubRequirementVM(int iLACertificationLinkId, double? cEHHours, List<ILACertificationSubRequirementLinkVM> iLACertificationSubRequirementLinkVM)
        {
            ILACertificationLinkId = iLACertificationLinkId;
            CEHHours = cEHHours;
            ILACertificationSubRequirementLinkVM = iLACertificationSubRequirementLinkVM??new List<ILACertificationSubRequirementLinkVM>();
        }
    }

    public class ILACertificationSubRequirementLinkVM
    {
        public int ILACertificationSubRequirementLinkId { get; set; }
        public string ReqName { get; set; }
        public double? ReqHours { get; set; }
        public ILACertificationSubRequirementLinkVM() { }
        public ILACertificationSubRequirementLinkVM(int iLACertificationSubRequirementLinkId,string reqName, double? reqHours)
        {
            ILACertificationSubRequirementLinkId = iLACertificationSubRequirementLinkId;
            ReqName = reqName;
            ReqHours = reqHours;
        }
    }
}
