using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Interfaces
{
	public interface IReportModelGenerator
	{
		Task<IReportModel> GenerateModel(Report report);
	}
}
