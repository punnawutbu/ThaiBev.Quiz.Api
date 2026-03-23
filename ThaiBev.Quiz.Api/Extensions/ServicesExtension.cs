using ThaiBev.Quiz.Api.Models;
using ThaiBev.Quiz.Api.Shared.Services;

namespace ThaiBev.Quiz.Api.Extensions
{
    public static class ServicesExtension
    {
        public static void ConfigureScopeFacades(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddScoped<IQuestionService, QuestionService>();
        }
    }
}