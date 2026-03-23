namespace ThaiBev.Quiz.Api.Shared.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int ChoiceNo { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}