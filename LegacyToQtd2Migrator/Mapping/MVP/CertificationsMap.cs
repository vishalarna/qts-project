using LegacyToQtd2Migrator.Legacy.Data;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyToQtd2Migrator.Mapping.MVP
{
    public class CertificationsMap : Common.MigrationMap<LkTblCertificationType, Certification>
    {
        List<LkTblCertificationType> _certs;

        public CertificationsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LkTblCertificationType> getSourceRecords()
        {
            _certs = (_source as EMP_DemoContext).LkTblCertificationTypes.ToListAsync().Result;
            return _certs;
        }

        protected override Certification mapRecord(LkTblCertificationType obj)
        {
            return new Certification()
            {
                CertifyingBodyId = 1,
                CertAcronym = obj.CertAbbrev,
                Name = obj.CertDesc,
                CertDesc = obj.CertDesc,
                AdditionalHours = obj.Nercpolicy,
                CreditHrsReq = obj.TotalReqCehs.HasValue,
                CreditHrs = obj.TotalReqCehs.GetValueOrDefault(), 
                CertificationSubRequirements = getSubRequirements(obj),
                CertSubReq = true,
                AllowRolloverHours = true,
                RenewalDate = true,
                ExpirationDate = true,
                RenewalTimeFrame = true,
                RenewalInterval = 3,
                Active = true,
                Deleted = false,
                CertMemberNum = true,
                CertifiedDate = true,
            };
        }
        private ICollection<CertificationSubRequirement> getSubRequirements(LkTblCertificationType obj)
        {
            List<CertificationSubRequirement> requirements = new List<CertificationSubRequirement>();

            requirements.Add(new CertificationSubRequirement()
            {
                Active = true,
                ReqHour = obj.Nercpolicy.GetValueOrDefault(),
                ReqName = "Standards"
            });

            requirements.Add(new CertificationSubRequirement()
            {
                Active = true,
                ReqHour = obj.CredReqHours.GetValueOrDefault(),
                ReqName = "Simulations"
            });

            //requirements.Add(new CertificationSubRequirement()
            //{
            //    Active = true,
            //    ReqHour = obj.TotalReqCehs.GetValueOrDefault(),
            //    ReqName = "Operation CEHs"
            //});

            return requirements;
        }

        private ICollection<Certification_History> getCertification_Histories()
        {
            List<Certification_History> certification_Histories = new List<Certification_History>();

            certification_Histories.Add(new Certification_History()
            {
                //CertId
                //EffectiveDate
                //Notes
                Deleted = false,
                Active = true
            });

            return certification_Histories;
        }

        protected override void setTotalRecordsToConvert()
        {
            TotalRecordsToConvert = _certs.Count();
        }

        protected override void updateTarget(Certification record)
        {
            (_target as QTD2.Data.QTDContext).Certifications.Add(record);
        }
    }
}