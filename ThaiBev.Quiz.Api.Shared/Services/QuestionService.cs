using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThaiBev.Quiz.Api.Shared.Data;
using ThaiBev.Quiz.Api.Shared.Models;

namespace ThaiBev.Quiz.Api.Shared.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _db;

        public QuestionService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Question>> GetAllQuestionAsync()
        {
            return await _db.Questions
                .Include(q => q.Choices)
                .OrderBy(q => q.QuestionNo)
                .ToListAsync();
        }
        public async Task<Question> CreateQuestionAsync(string text, IEnumerable<string> choices)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Question is required.");

            if (choices == null || choices.Count() != 4 || choices.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("Exactly 4 choices are required.");

            var nextNo = await _db.Questions.CountAsync() + 1;

            var question = new Question
            {
                QuestionNo = nextNo,
                Text = text.Trim(),
                Choices = choices.Select((choice, index) => new Choice
                {
                    ChoiceNo = index + 1,
                    Text = choice.Trim()
                }).ToList()
            };

            _db.Questions.Add(question);
            await _db.SaveChangesAsync();

            return question;
        }
        public async Task<bool> DeleteQuestionsAsync(int id)
        {
            var question = await _db.Questions
                .Include(q => q.Choices)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
                return false;

            _db.Questions.Remove(question);
            await _db.SaveChangesAsync();

            var remaining = await _db.Questions
                .OrderBy(q => q.QuestionNo)
                .ToListAsync();

            for (int i = 0; i < remaining.Count; i++)
            {
                remaining[i].QuestionNo = i + 1;
            }

            await _db.SaveChangesAsync();
            return true;
        }
    }
}