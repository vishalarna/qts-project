#nullable enable
using System.Text.Json.Serialization;

namespace QTD2.Infrastructure.Model
{
    public class TimezoneModel
    {

        public string Timezone { get; set; }
        public string StandardAbbreviation { get; set; }
        public string DaylightAbbreviation { get; set; }

        public TimezoneModel()
        {
        }
        public TimezoneModel(string timezone, string standardAbbreviation, string daylightAbbreviation)
        {
            Timezone = timezone;
            StandardAbbreviation = standardAbbreviation;
            DaylightAbbreviation = daylightAbbreviation;
        }
    }

}
