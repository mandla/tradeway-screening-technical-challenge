using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class QuizResponseController : ControllerBase
{
    private readonly DemoContext dc;

    public QuizResponseController(DemoContext dc)
    {
        this.dc = dc;
    }

    public class QuestionAnswerPair
    {
        public int question { get; set; }
        public int option { get; set; }
    }

    /// <summary>
    /// This method should add a 'Result' from someone taking the quiz,
    /// along with each of the answers they gave to whichever questions they were asked.
    /// </summary>
    [HttpPut]
    public IActionResult AddQuizResponse(string userName, List<QuestionAnswerPair> qAndOList)
    {
        var x = new QuizResponse
        {
            UserName = userName
        };

        dc.QuizResponses.Add(x);
        dc.SaveChanges();

        qAndOList.ForEach(q =>
        {
            var a = new QuestionAnswer
            {
                QuestionId = q.question,
                QuestionOptionId = q.option,
                QuizResponseId = x.Id
            };
            dc.QuestionAnswers.Add(a);
        });

        dc.SaveChanges();
        return Ok();
    }

    [HttpGet]
    public IActionResult TopTen()
    {
        // find the top 10 results
        var group = dc.QuestionAnswers.GroupBy(a => a.QuizResponseId).Select(b => new {
            total = b.Where(s => s.QuestionOption.QuestionId == s.QuestionId).Count(v => v.QuestionOption.IsCorrect),
            questionCount = b.Count(),
            name = b.First().QuizResponse.UserName,

        }).Select(d => new {
            totalCorrectAnswers = d.total,
            userName = d.name,
            questionCount = d.questionCount,
            scorePercentage = ((double)d.total / d.questionCount) * 100
        });

        return Ok(group.OrderByDescending(w => w.totalCorrectAnswers).ThenByDescending(h => h.userName).Take(10).ToList());
    }
}