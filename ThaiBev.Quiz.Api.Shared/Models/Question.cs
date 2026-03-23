namespace ThaiBev.Quiz.Api.Shared.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuestionNo { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }
    public class CreateQuestionReq
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Choices { get; set; } = new List<string>();
    }
}