using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Certifications.Factories.Interfaces
{
    public interface ICertificationFulfillmentCalculatorFactory
    {
        CertificationCalculatorFulfillmentType GetCertificationCalculatorFulfillmentType(Entities.Core.EmployeeCertification employeeCertification);
        ICertificationFulfillmentCalculator CreateCertificationFulfillmentCalculator(Entities.Core.EmployeeCertification employeeCertification);
        ICertificationFulfillmentCalculator CreateCertificationFulfillmentCalculator(CertificationCalculatorFulfillmentType certificationCalculatorFulfillmentType);
        ICertificationFulfillmentCalculator CreateBasicCalculator();
        ICertificationFulfillmentCalculator CreateNercCalculator();
        ICertificationFulfillmentCalculator CreateEmergencyResponseCalculator();

    }
}
