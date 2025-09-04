using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ILACertificationLink : Common.Entity
    {

        public int CertificationId { get; set; }
        public int ILAId { get; set; }

        //public double TotalHours { get; set; }
    
        public bool IsIncludeSimulation { get; set; }
        public bool IsEmergencyOpHours { get; set; }
        public bool IsPartialCreditHours { get; set; }

        public double? CEHHours { get; set; }

        public virtual ILA ILA { get; set; }
        public virtual Certification Certification { get; set; }
        public virtual ICollection<ILACertificationSubRequirementLink> ILACertificationSubRequirementLink { get; set; } = new List<ILACertificationSubRequirementLink>();
        public virtual ICollection<ClassScheduleEmployee_ILACertificationLink_PartialCredit> ClassScheduleEmployee_ILACertificationLink_PartialCredits { get; set; } = new List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>();

        public ILACertificationLink(int certificationId, int ilaId, bool isIncludeSimulation, bool isEmergencyOpHours, bool isPartialCreditHours, double? cehHours)
        {
            CertificationId = certificationId;
            ILAId = ilaId;
            IsIncludeSimulation = isIncludeSimulation;
            IsEmergencyOpHours = isEmergencyOpHours;
            IsPartialCreditHours = isPartialCreditHours;
            CEHHours = cehHours;
        }
        public ILACertificationLink()
        {

        }

        public override T Copy<T>(string createdBy)
        {
            var copy =  base.Copy<T>(createdBy) as ILACertificationLink;

            foreach(var iLACertificationSubRequirementLink in this.ILACertificationSubRequirementLink)
            {
                var iLACertificationSubRequirementLinkCopy = iLACertificationSubRequirementLink.Copy<ILACertificationSubRequirementLink>(createdBy);
                iLACertificationSubRequirementLinkCopy.ILACertificationLinkId = 0;
                copy.ILACertificationSubRequirementLink.Add(iLACertificationSubRequirementLinkCopy);
            }

            return (T)(object)copy;
        }

        public void UpdateCertificationLink(bool isIncludeSimulation, bool isEmergencyOpHours, bool isPartialCreditHours, double? cehHours)
        {
            IsIncludeSimulation = isIncludeSimulation;
            IsEmergencyOpHours = isEmergencyOpHours;
            IsPartialCreditHours = isPartialCreditHours;
            CEHHours = cehHours;
        }

        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnlaILACertificationLinkDeleted(this));
            base.Delete();
        }
    }
}
