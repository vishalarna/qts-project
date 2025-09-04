using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using RazorEngine;
using RazorEngine.Templating;

using QTD2.Domain.Interfaces.Service.Core;

namespace QTD2.Infrastructure.Notification.Content
{
    public class RazorEmailContentGenerator : IContentGenerator
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public RazorEmailContentGenerator(
            IHostingEnvironment hostingEnvironment
            )
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetContent(string templatePath, object model)
        {
            var t = File.ReadAllText(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), templatePath));

            string content = "";

            if (Engine.Razor.IsTemplateCached(templatePath, model?.GetType()))
                content = Engine.Razor.Run(templatePath, model?.GetType(), model);

            else
                content = Engine.Razor.RunCompile(t, templatePath, model?.GetType(), model);

            return content;
        }

        public string GetContent(string templateName, string template, object model)
        {
            var t = File.ReadAllText(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Notification/Content/Views/_EmailLayout.cshtml"));
            var tFinal = t.Replace("{{template}}", template);
            string content;
            string uniqueTemplateName = $"{templateName}_{Guid.NewGuid()}";
            content = Engine.Razor.RunCompile(tFinal, uniqueTemplateName, model?.GetType(), model);
            return content;
        }
    }
}
