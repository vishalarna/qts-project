using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Reports.Interfaces;

using System.Threading.Tasks;

namespace QTD2.Infrastructure.Reports.Generation
{
    public class ReportGenerator : IReportGenerator
    {
        IReportModelFactory _modelFactory;
        IReportContentGenerator _contentGenerator;

        public ReportGenerator(IReportModelFactory modelFactory, IReportContentGenerator contentGenerator)
        {
            _modelFactory = modelFactory;
            _contentGenerator = contentGenerator;
        }

        public async Task<string> GenerateReport(Report report)
        {
            
            var model = await _modelFactory.GenerateModelAsync(report);
            var content = _contentGenerator.GetReportContent(model);
            return content;
        }
    }
}
