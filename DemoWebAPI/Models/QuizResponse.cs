namespace DemoWebAPI.Models;

public class QuizResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public List<QuestionAnswer>? QuestionAnswers { get; set; }
}