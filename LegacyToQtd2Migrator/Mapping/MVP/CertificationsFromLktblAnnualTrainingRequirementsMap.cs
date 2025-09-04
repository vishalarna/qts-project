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
    public class CertificationsFromLktblAnnualTrainingRequirementsMap : Common.MigrationMap<LktblAnnualTrainingRequirement, Certification>
    {
        List<LktblAnnualTrainingRequirement> _certs;
        List<TblLabelReplacementText> _labelReplacements;

        public CertificationsFromLktblAnnualTrainingRequirementsMap(DbContext source, DbContext target) : base(source, target)
        {

        }

        protected override List<LktblAnnualTrainingRequirement> getSourceRecords()
        {
            _certs = (_source as EMP_DemoContext).LktblAnnualTrainingRequirements.ToList();
            _labelReplacements = (_source as EMP_DemoContext).TblLabelReplacementTexts.ToList();
            return _certs;
        }

        protected override Certification mapRecord(LktblAnnualTrainingRequirement obj)
        {
            string legacyName = getLegacyName(obj.TrainingType);
            var labelreplacement = _labelReplacements.Where(r => r.DefaultText.ToUpper() == legacyName.ToUpper()).FirstOrDefault();

            string name = labelreplacement == null ? obj.TrainingType : labelreplacement.ReplacementText;

            return new Certification()
            {
                CertifyingBodyId = 2,
                CertAcronym = name,
                Name = name,
                CertDesc = legacyName,
                AdditionalHours = 0,
                AllowRolloverHours = false,
                CreditHrsReq = obj.TrainingHours.HasValue,
                CreditHrs = obj.TrainingHours.GetValueOrDefault(),
                CertificationSubRequirements = getSubRequirements(obj),
                CertSubReqHours = 0,
                RenewalInterval = 1,
                CertSubReq = false,
                Active = true,
                Deleted = false,
                CertMemberNum = true,
                CertifiedDate = true,
                RenewalDate = true,
                ExpirationDate = true,
            };
        }

        private string getLegacyName(string trainingType)
        {
            switch (trainingType.ToUpper())
            {
                case "EMERGENCYRESPONCE":
                    return "Emergency Response";
                case "REGIONALTRAINING":
                    return "Reg";
                case "REGIONALTRAINING2":
                    return "Reg2";
                default: return trainingType;
            }
        }

        private ICollection<CertificationSubRequirement> getSubRequirements(LktblAnnualTrainingRequirement obj)
        {
            List<CertificationSubRequirement> subRequirements = new List<CertificationSubRequirement>();

            //subRequirements.Add(new CertificationSubRequirement()
            //{
            //    ReqHour = obj.TrainingHours.GetValueOrDefault(),
            //    ReqName = "Operation CEHs"
            //});

            return subRequirements;
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
