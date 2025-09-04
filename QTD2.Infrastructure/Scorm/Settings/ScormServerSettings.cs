namespace QTD2.Infrastructure.Scorm.Settings
{
    public class ScormServerSettings
    {
        public string FullApiPath { get { return Domain + APIPath; } }
        public string APIPath { get; set; }
        public string Domain { get; set; }
        public bool UseMock { get; set; }
    }
}
