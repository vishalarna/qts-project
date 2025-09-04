using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace QTD2.Application.Utils
{
    public static class Utils
    {
        public static T DeepCopy<T>(this T self)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            };

            var serialized = JsonSerializer.Serialize(self, options);
            return JsonSerializer.Deserialize<T>(serialized, options);
        }

    }
}
