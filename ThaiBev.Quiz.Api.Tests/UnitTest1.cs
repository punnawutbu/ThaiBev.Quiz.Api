using ThaiBev.Quiz.Api.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ThaiBev.Quiz.Api.Shared.Data;
using ThaiBev.Quiz.Api.Shared.Services;
namespace ThaiBev.Quiz.Api.Tests;

public class UnitTest1
{
    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
    [Fact]
    public async Task GetAllQuestionAsync()
    {
        await using var db = CreateDbContext();

        db.Questions.AddRange(
            new Question
            {
                QuestionNo = 2,
                Text = "Q2",
                Choices = new List<Choice>
                {
                        new() { ChoiceNo = 1, Text = "A" },
                        new() { ChoiceNo = 2, Text = "B" },
                        new() { ChoiceNo = 3, Text = "C" },
                        new() { ChoiceNo = 4, Text = "D" }
                }
            },
            new Question
            {
                QuestionNo = 1,
                Text = "Q1",
                Choices = new List<Choice>
                {
                        new() { ChoiceNo = 1, Text = "A" },
                        new() { ChoiceNo = 2, Text = "B" },
                        new() { ChoiceNo = 3, Text = "C" },
                        new() { ChoiceNo = 4, Text = "D" }
                }
            }
        );

        await db.SaveChangesAsync();

        var service = new QuestionService(db);

        var result = (await service.GetAllQuestionAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].QuestionNo);
        Assert.Equal(2, result[1].QuestionNo);
    }

    [Fact]
    public async Task CreateQuestionAsync()
    {
        await using var db = CreateDbContext();
        var service = new QuestionService(db);

        var result = await service.CreateQuestionAsync(
            "What is 2+2?",
            new[] { "1", "2", "3", "4" });

        Assert.NotNull(result);
        Assert.Equal(1, result.QuestionNo);
        Assert.Equal("What is 2+2?", result.Text);
        Assert.Equal(4, result.Choices.Count());
    }

    [Fact]
    public async Task DeleteQuestionsAsync()
    {
        await using var db = CreateDbContext();

        db.Questions.AddRange(
            new Question
            {
                QuestionNo = 1,
                Text = "Q1",
                Choices = new List<Choice>
                {
                        new() { ChoiceNo = 1, Text = "A" },
                        new() { ChoiceNo = 2, Text = "B" },
                        new() { ChoiceNo = 3, Text = "C" },
                        new() { ChoiceNo = 4, Text = "D" }
                }
            },
            new Question
            {
                QuestionNo = 2,
                Text = "Q2",
                Choices = new List<Choice>
                {
                        new() { ChoiceNo = 1, Text = "A" },
                        new() { ChoiceNo = 2, Text = "B" },
                        new() { ChoiceNo = 3, Text = "C" },
                        new() { ChoiceNo = 4, Text = "D" }
                }
            },
            new Question
            {
                QuestionNo = 3,
                Text = "Q3",
                Choices = new List<Choice>
                {
                        new() { ChoiceNo = 1, Text = "A" },
                        new() { ChoiceNo = 2, Text = "B" },
                        new() { ChoiceNo = 3, Text = "C" },
                        new() { ChoiceNo = 4, Text = "D" }
                }
            }
        );

        await db.SaveChangesAsync();

        var q2 = await db.Questions.FirstAsync(q => q.QuestionNo == 2);
        var service = new QuestionService(db);

        var deleted = await service.DeleteQuestionsAsync(q2.Id);

        Assert.True(deleted);

        var remaining = await db.Questions
            .OrderBy(q => q.QuestionNo)
            .ToListAsync();

        Assert.Equal(2, remaining.Count);
        Assert.Equal(1, remaining[0].QuestionNo);
        Assert.Equal(2, remaining[1].QuestionNo);
        Assert.Equal("Q3", remaining[1].Text);
    }
}
