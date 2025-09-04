using System;
using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Certification : Common.Entity
    {
        public int CertifyingBodyId { get; set; }
        public string CertAcronym { get; set; }
        public string Name { get; set; }
        public string CertDesc { get; set; }

        public bool? RenewalTimeFrame { get; set; }
        public int? RenewalInterval { get; set; }

        public bool? CreditHrsReq { get; set; }
        public float? CreditHrs { get; set; }

        public bool? CertSubReq { get; set; }

        public string? CertSubReqName { get; set; }
        public float? CertSubReqHours { get; set; }

        public bool? CertMemberNum { get; set; }
        public bool? CertifiedDate { get; set; }
        public bool? RenewalDate { get; set; }
        public bool? ExpirationDate { get; set; }
        public bool? AllowRolloverHours { get; set; }
        public float? AdditionalHours { get; set; }

        public DateTime EffectiveDate { get; set; }
        public string? InternalIdentifier { get; set; }

        public virtual CertifyingBody CertifyingBody { get; set; }

        public virtual ICollection<Certification_History> Certifications_Histories { get; set; } = new List<Certification_History>();

        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; } = new List<EmployeeCertification>();

        //public Certification(int certifyingBodyId, string certacronym, string name, string description, bool renewaltimeframe, int renewalinterval, bool credithrsreq, float credithrs, bool certsubreq,
        //                        string certsubreqname, float certsubreqhours, bool certmembernum, bool certifieddate, bool renewaldate, bool expirationdate, bool allowrolloverhours,
        //                             float additionalhours);
       
        public virtual ICollection<ILACertificationLink> ILACertificationLinks { get; set; } = new List<ILACertificationLink>();

        public virtual ICollection<CertificationSubRequirement> CertificationSubRequirements { get; set; } = new List<CertificationSubRequirement>();


        public Certification(int certifyingBodyId, string certacronym, string name, string description, bool? renewaltimeframe, int? renewalinterval, bool? credithrsreq, float? credithrs, bool? certsubreq,
                    string? certsubreqname, float? certsubreqhours, bool? certmembernum, bool? certifieddate, bool? renewaldate, bool? expirationdate, bool? allowrolloverhours,
                    float? additionalhours, DateTime effectiveDate)
        {
            CertifyingBodyId = certifyingBodyId;
            Name = name;
            CertAcronym = certacronym;
            CertDesc = description;
            RenewalTimeFrame = renewaltimeframe;
            RenewalInterval = renewalinterval;
            CreditHrsReq = credithrsreq;
            CreditHrs = credithrs;
            CertSubReq = certsubreq;
            CertSubReqName = certsubreqname;
            CertSubReqHours = certsubreqhours;
            CertMemberNum = certmembernum;
            CertifiedDate = certifieddate;
            RenewalDate = renewaldate;
            ExpirationDate = expirationdate;
            AllowRolloverHours = allowrolloverhours;
            AdditionalHours = additionalhours;
            EffectiveDate = effectiveDate;
        }

        public Certification()
        {
        }
        public override void Delete()
        {
            AddDomainEvent(new Domain.Events.Core.OnCertificationDeleted(this));
            base.Delete();
        }
    }
}
