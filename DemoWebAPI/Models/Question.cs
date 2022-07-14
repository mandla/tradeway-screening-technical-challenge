namespace DemoWebAPI.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    
    public ICollection<QuestionOption>? QuestionOptions { get; set; }
}