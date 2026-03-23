using ThaiBev.Quiz.Api.Shared.Models;

namespace ThaiBev.Quiz.Api.Models
{
    public class AppSettings
    {
        public string credentialTxt { get; set; }
        public CredentialSetting CredentialSetting { get; set; }
        public SwaggerSettings Swagger { get; set; } = new();
    }
}