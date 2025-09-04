using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using QTD2.Domain.Exceptions;

namespace QTD2.Domain.Certifications.Factories.Implimentations
{
    public class CertificationFulfillmentCalculatorFactory : Interfaces.ICertificationFulfillmentCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CertificationFulfillmentCalculatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CertificationCalculatorFulfillmentType GetCertificationCalculatorFulfillmentType(EmployeeCertification employeeCertification)
        {
            string certBodyName = employeeCertification.Certification.CertifyingBody.Name.ToUpper();

            switch (certBodyName)
            {
                case "NERC":
                    return CertificationCalculatorFulfillmentType.Nerc;
                default:
                    if (employeeCertification.Certification.InternalIdentifier == "Emergency Response")
                    {
                        return CertificationCalculatorFulfillmentType.EmergencyResponse;
                    }
                    else
                    {
                        return CertificationCalculatorFulfillmentType.Basic;
                    }
            }
        }

        public ICertificationFulfillmentCalculator CreateCertificationFulfillmentCalculator(EmployeeCertification employeeCertification)
        {
            CertificationCalculatorFulfillmentType t = GetCertificationCalculatorFulfillmentType(employeeCertification);
            return CreateCertificationFulfillmentCalculator(t);
        }

        public ICertificationFulfillmentCalculator CreateBasicCalculator()
        {
            return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.BasicCertificationFulfillmentCalculator>();
        }

        public ICertificationFulfillmentCalculator CreateNercCalculator()
        {
            return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.NercCertificationFulfillmentCalculator>();
        }

        public ICertificationFulfillmentCalculator CreateCertificationFulfillmentCalculator(CertificationCalculatorFulfillmentType certificationCalculatorFulfillmentType)
        {
            switch (certificationCalculatorFulfillmentType)
            {
                case CertificationCalculatorFulfillmentType.Basic:
                    return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.BasicCertificationFulfillmentCalculator>();
                case CertificationCalculatorFulfillmentType.Nerc:
                    return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.NercCertificationFulfillmentCalculator>();
                case CertificationCalculatorFulfillmentType.EmergencyResponse:
                    return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.EmergencyResponseCertificationFulfillmentCalculator>();
                default:
                    throw new QTDServerException("Fulfillment calculator factory not implimented");
            }
        }

        public ICertificationFulfillmentCalculator CreateEmergencyResponseCalculator()
        {
            return _serviceProvider.GetRequiredService<Certifications.Implimentations.FulfillmentCalculators.EmergencyResponseCertificationFulfillmentCalculator>();
        }
    }
}
