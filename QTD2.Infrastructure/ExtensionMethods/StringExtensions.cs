using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;

namespace QTD2.Infrastructure.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string StripHTML(this string str)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(str);

                return doc.DocumentNode.InnerText;
            }
            catch (Exception e)
            {
                return str;
            }            
        }

        public static string CleanCBTScormQuestionChoice(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            string cleanedInput = input.Replace("urn:scormdriver:", "");

            cleanedInput = Regex.Replace(cleanedInput, @"\[\s*,\s*\]", ", ");

            return cleanedInput;
        }
    }
}
