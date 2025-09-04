namespace QTD2.Domain.Certifications.Models
{
    public class CertifyingBodyInconsistency
    {
        public string Name { get; }
        public string Message { get; }
        public bool DisplayName { get; }

        public CertifyingBodyInconsistency(string name, string message, bool displayName)
        {
            Name = name;
            Message = message;
            DisplayName = displayName;
        }
    }
}
