
namespace ThaiBev.Quiz.Api.Shared.Models
{
    public class CredentialSetting
    {
        public string ConnectionString { get; set; }
        public string SecertKey { get; set; }
        public string ServiceKey { get; set; }
        public bool SslMode { get; set; }
        // public Certificate Certificate { get; set; } //Lib
    }
}