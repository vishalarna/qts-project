using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Certifications.Models;

namespace QTD2.Infrastructure.Reports.Generation.Generators.Helpers.Interfaces
{
	public interface ICertificationReportHelper
	{
		Task<List<CertificationFulfillmentStatus>> GetCertificationFulfillmentStatuses(List<int> employeeIds, List<int> certificationIds);
		Task<List<CertificationFulfillmentStatus>> GetCertificationFulfillmentStatuses(List<int> employeeIds);
	}
}
