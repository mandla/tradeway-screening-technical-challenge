namespace DemoWebAPI.Models;

public class QuestionAnswer
{
    public int Id { get; set; }
    public int QuizResponseId { get; set; }
    public QuizResponse QuizResponse { get; set; } = null!;
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public int QuestionOptionId { get; set; }
    public QuestionOption QuestionOption { get; set;  } = null!;
}