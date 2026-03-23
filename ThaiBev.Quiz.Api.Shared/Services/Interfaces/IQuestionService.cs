using ThaiBev.Quiz.Api.Shared.Models;

namespace ThaiBev.Quiz.Api.Shared.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestionAsync();
        Task<Question> CreateQuestionAsync(string text, IEnumerable<string> choices);
        Task<bool> DeleteQuestionsAsync(int id);
    }
}