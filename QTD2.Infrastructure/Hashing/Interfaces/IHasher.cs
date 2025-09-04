namespace QTD2.Infrastructure.Hashing.Interfaces
{
    public interface IHasher
    {
        string Encode(string value, string saltPrefix = "");

        string Decode(string encoded, string saltPrefix = "");
    }
}
