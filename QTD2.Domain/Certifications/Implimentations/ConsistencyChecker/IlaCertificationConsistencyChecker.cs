using QTD2.Domain.Certifications.Interfaces;
using QTD2.Domain.Certifications.Models;
using QTD2.Domain.Entities.Core;
using System.Linq;

using System.Collections.Generic;
using System.ComponentModel.Design;
using System;

namespace QTD2.Domain.Certifications.Implimentations.ConsistencyChecker
{
    public class ILACertificationConsistencyChecker : IILACertificationConsistencyChecker
    {
        public List<CertifyingBodyConsistencyResult> CheckCertificationConsistency(ILA ila)
        {
            if (ila.ILACertificationLinks.Any(icl => icl.Certification == null || icl.Certification.CertifyingBody == null || icl.Certification.CertifyingBody.Certifications.Count() == 0))
            {
                throw new Exception("ILA not loaded correctly to use CheckCertificationConsistency");
            }

            List<CertifyingBody> certifyingBodies = ila.ILACertificationLinks
                .Where(icl => icl.Certification != null)
                .Select(icl => icl.Certification)
                .Where(c => c?.CertifyingBody != null)
                .Select(c => c.CertifyingBody)
                .Where(r => r.Active).Distinct().ToList();
            List<CertifyingBodyConsistencyResult> results = new List<CertifyingBodyConsistencyResult>();

            foreach (var certifyingBody in certifyingBodies)
            {
                results.Add(CheckCertificationConsistency(ila, certifyingBody));
            }

            return results;
           
        }

        public CertifyingBodyConsistencyResult CheckCertificationConsistency(ILA ila, CertifyingBody certifyingBody)
        {
            if (ila.ILACertificationLinks.Any(icl => icl.Certification == null || icl.Certification.CertifyingBody == null))
            {
                throw new Exception("ILA not loaded correctly to use CheckCertificationConsistency");
            }
            if (certifyingBody.Certifications.Count() == 0)
            {
                throw new Exception($"CertifyingBody {certifyingBody.Name} not loaded correctly to use CheckCertificationConsistency");
            }

            var result = new CertifyingBodyConsistencyResult(certifyingBody);

            // We only care about checking consistency for Certifications that are managed by the CertifyingBody as a whole 
            if (certifyingBody.EnableCertifyingBodyLevelCEHEditing) {

                List<Entities.Core.CertificationSubRequirement> certificationSubRequirements = null;

                var firstILACertificationLink = ila.ILACertificationLinks.Where(r => r.Certification.Active && r.Certification.CertifyingBodyId == certifyingBody.Id).FirstOrDefault();

                List<ILACertificationSubRequirementLink> ilaCertificationSubRequirementLinks = firstILACertificationLink?.ILACertificationSubRequirementLink.ToList() ?? null;
                bool isIncludeSimulation = firstILACertificationLink?.IsIncludeSimulation ?? false; // Its ok to default to false, if no ILACertificationLinks exist for the CertifyingBody, we return "Certification not linked to ILA" anyway 
                bool isEmergencyOpHours = firstILACertificationLink?.IsEmergencyOpHours ?? false; // Its ok to default to false, if no ILACertificationLinks exist for the CertifyingBody, we return "Certification not linked to ILA" anyway
                bool isPartialCreditHours = firstILACertificationLink?.IsPartialCreditHours ?? false; // Its ok to default to false, if no ILACertificationLinks exist for the CertifyingBody, we return "Certification not linked to ILA" anyway
                double? cehHours = firstILACertificationLink?.CEHHours ?? null; // Its ok to default to null, if no ILACertificationLinks exist for the CertifyingBody, we return "Certification not linked to ILA" anyway

                foreach (var certification in certifyingBody.Certifications.Where(r => r.Active))
                {
                    // Check that the Certification is defined the same as other Certifications in the CertifyingBody (meaning we see the same CertificationSubRequirements amonst all Certifications)
                    if (certificationSubRequirements == null)
                    {
                        certificationSubRequirements = certification.CertificationSubRequirements.ToList();
                    }
                    if (certification.CertificationSubRequirements.Any(csr => !certificationSubRequirements.Any(csr2 => csr.ReqName == csr2.ReqName)) ||
                        certificationSubRequirements.Any(csr => !certification.CertificationSubRequirements.Any(csr2 => csr.ReqName == csr2.ReqName)))
                    {
                        result.SetInconsistency(certification.Name, "Certification has different CertificationSubRequirements compared to other Certifications.", false);
                    }

                    var ilaCertificationLink = ila.ILACertificationLinks
                                                    .Where(r => r.Active)
                                                    .Where(r => r.CertificationId == certification.Id)
                                                .FirstOrDefault();

                    if (ilaCertificationLink == null)
                    {
                        result.SetInconsistency(certification.Name, "Certification not linked to ILA.", true);
                        continue;
                    }

                    // Check that all the ILACertificationLink SubRequirements exist, and that all SubRequirements have the same ReqHours
                    if (ilaCertificationSubRequirementLinks == null)
                    {
                        ilaCertificationSubRequirementLinks = ilaCertificationLink.ILACertificationSubRequirementLink.ToList();
                    }
                    if (ilaCertificationLink.ILACertificationSubRequirementLink.Any(icsr => !ilaCertificationSubRequirementLinks.Any(icsr2 => icsr.CertificationSubRequirement.ReqName == icsr2.CertificationSubRequirement.ReqName)) ||
                        ilaCertificationSubRequirementLinks.Any(icsr => !ilaCertificationLink.ILACertificationSubRequirementLink.Any(icsr2 => icsr.CertificationSubRequirement.ReqName == icsr2.CertificationSubRequirement.ReqName)))
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different ILACertificationSubRequirements compared to other ILACertificationLinks.", false);
                    }
                    if (ilaCertificationLink.ILACertificationSubRequirementLink.Any(icsr => ilaCertificationSubRequirementLinks.Any(icsr2 => icsr.CertificationSubRequirement.ReqName == icsr2.CertificationSubRequirement.ReqName && icsr.ReqHour != icsr2.ReqHour)))
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different ILACertificationSubRequirement ReqHours compared to other ILACertificationLinks.", false);
                    }

                    // Check that all the ILACertificationLink fields have the same values
                    if (isIncludeSimulation != ilaCertificationLink.IsIncludeSimulation)
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different isIncludeSimulation compared to other ILACertificationLinks.", false);
                    }
                    if (isEmergencyOpHours != ilaCertificationLink.IsEmergencyOpHours)
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different isEmergencyOpHours compared to other ILACertificationLinks.", false);
                    }
                    if (isPartialCreditHours != ilaCertificationLink.IsPartialCreditHours)
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different isPartialCreditHours compared to other ILACertificationLinks.", false);
                    }
                    if (cehHours != ilaCertificationLink.CEHHours)
                    {
                        result.SetInconsistency(certification.Name, "ILACertificationLink has different cehHours compared to other ILACertificationLinks.", false);
                    }
                }
            }

            return result;
        }
    }
}
