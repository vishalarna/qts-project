using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using QTD2.Domain.Entities.Core;

namespace QTD2.Infrastructure.Reports.Generation
{
    public static class LabelReplacementHelper
    {
        public static string ReplaceLabel(this string str, List<ClientSettings_LabelReplacement> replacements)
        {
            var label = replacements.FirstOrDefault(x => x.DefaultLabel == str);
            return (label != null && !string.IsNullOrEmpty(label.LabelReplacement)) ? label.LabelReplacement : str;
        }

        public static string DynamicReplaceLabel(this string str, List<ClientSettings_LabelReplacement> replacements)
        {
            foreach (var replacement in replacements)
            {
                if (string.IsNullOrWhiteSpace(replacement.LabelReplacement) || replacement.LabelReplacement == null)
                {
                    replacement.LabelReplacement = replacement.DefaultLabel;
                }

                var regex = new Regex("\\b" + Regex.Escape(replacement.DefaultLabel), RegexOptions.IgnoreCase);
                str = regex.Replace(str, replacement.LabelReplacement);
            }

            return str;
        }
    }
}