using HashidsNet;
using Microsoft.Extensions.Options;
using QTD2.Infrastructure.Hashing.Interfaces;

namespace QTD2.Infrastructure.Hashing
{
    public class HashIdsHasher : IHasher
    {
        private readonly Settings.HashIdOptions _hashSettings;

        public HashIdsHasher(IOptions<Settings.HashIdOptions> hashOptions)
        {
            _hashSettings = hashOptions.Value;
        }

        public string Decode(string encoded, string saltPrefix = "")
        {
            if (!_hashSettings.ShouldHash)
            {
                return encoded;
            }

            if (string.IsNullOrEmpty(encoded))
                return null;

            var hashids = new Hashids($"{saltPrefix}{_hashSettings.Salt}");

            // please document that this only supports 1 ID at a time
            var results = hashids.Decode(encoded);

            if (results.Length != 1)
            {
                // I dont know if I like throwing this exception like this.  I hate the idea of the salt making it to the client because we didn't properly remove it when error handling.  I do want to log the salt server side though
                throw new System.Exception($"The hash could not be decoded. {System.Environment.NewLine} Value: {encoded} {System.Environment.NewLine} Salt: {saltPrefix}{_hashSettings.Salt}");
            }

            return results[0].ToString();
        }

        public string Encode(string value, string saltPrefix = "")
        {
            if (!_hashSettings.ShouldHash)
            {
                return value;
            }

            if (string.IsNullOrEmpty(value))
                return null;

            var hashids = new Hashids($"{saltPrefix}{_hashSettings.Salt}");
            var parsed = int.TryParse(value, out int id);

            if (!parsed)
            {
                // originally this threw an exception but sicne GUIDs can exist in the auth DB, its better to just return the value
                return value;
            }

            return hashids.Encode(id);
        }
    }
}
