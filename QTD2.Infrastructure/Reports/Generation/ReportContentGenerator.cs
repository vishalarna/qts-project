using QTD2.Infrastructure.Reports.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using Microsoft.Extensions.Options;
using System;

namespace QTD2.Infrastructure.Reports.Generation
{
    public class ReportContentGenerator : IReportContentGenerator
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _path;
        private readonly bool _shouldCache;

        public ReportContentGenerator(
           IHostingEnvironment hostingEnvironment,
           IOptions<ReportSettings> reportSettingsOptions
           )
        {
            _hostingEnvironment = hostingEnvironment;
            _path = reportSettingsOptions.Value.Path;
            _shouldCache = reportSettingsOptions.Value.ShouldCache;
        }

        public string GetReportContent(IReportModel model)
        {
            var file = Path.Combine(_path, model.TemplatePath);
            string templateName = _shouldCache ? model.TemplatePath : System.Guid.NewGuid().ToString();

            var t = File.ReadAllText(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), file));

            string content;
            try
            {
                if (Engine.Razor.IsTemplateCached(templateName, model.GetType()))
                    content = Engine.Razor.Run(templateName, model.GetType(), model);
                else
                    content = Engine.Razor.RunCompile(t, templateName, model.GetType(), model);
            }
            catch(Exception e)
            {
                content = Engine.Razor.RunCompile(t, templateName, model.GetType(), model);
            }

            return content;
        }
    }
}
