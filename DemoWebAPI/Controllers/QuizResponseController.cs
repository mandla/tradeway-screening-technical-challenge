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

    /// <summary>
    /// This method should add a 'Result' from someone taking the quiz,
    /// along with each of the answers they gave to whichever questions they were asked.
    /// </summary>
    [HttpPut]
    public IActionResult AddQuizResponse(string userName,int qI1, int o1, int qI2, int o2, int qI3, int o3, int qI4, int o4)
    {
        var x=new QuizResponse {UserName=userName};

        dc.QuizResponses.Add(x);
        dc.SaveChanges();

        var a=new QuestionAnswer{QuestionId=qI1,QuestionOptionId=o1,QuizResponseId=x.Id};
        dc.QuestionAnswers.Add(a);

        var b=new QuestionAnswer{QuestionId=qI2,QuestionOptionId=o2,QuizResponseId=x.Id};
        dc.QuestionAnswers.Add(b);

        var c=new QuestionAnswer{QuestionId=qI3,QuestionOptionId=o3,QuizResponseId=x.Id};
        dc.QuestionAnswers.Add(c);

        var d=new QuestionAnswer{QuestionId=qI4,QuestionOptionId=o4,QuizResponseId=x.Id};
        dc.QuestionAnswers.Add(d);

        dc.SaveChanges();
        return Ok();
    }

    [HttpGet]
    public IActionResult TopTen()
    {
        // find the top 10 results
        return NoContent();
    }
}