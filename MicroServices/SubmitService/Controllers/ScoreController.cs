using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/Score/[Action]")]
public class ScoreController : Controller
{
    private readonly Context db;
    public ScoreController(Context _db)
    {
        db = _db;
    }

    [HttpGet]
    public IActionResult GetStudentScores(int StudentId, int ClassId)
    {
        var result = db.Scores_tbl
            .Where(x => x.ClassId == ClassId && x.StudentId == StudentId)
            .Include(x => x.Class)
            .Include(x => x.Student)
            .FirstOrDefault();

        if (result == null)
        {
            return BadRequest("اطلاعات نادرست است .");
        }

        return Ok(new ScoreResult
        {
            StudentName = result.Student.FirstName + " " + result.Student.LastName,
            ClassName = result.Class.ClassName,
            StudentScore = result.StudentScore,
            StudentId = result.StudentId,
            ClassId = result.ClassId
        });
    }
    [HttpGet]
    public IActionResult GetStudentAllScores(int StudentId)
    {
        var result = db.Students_tbl
            .Where(x => x.Id == StudentId)
            .Include(x => x.Scores)
                .ThenInclude(x => x.Class)
            .FirstOrDefault();

        if (result == null)
        {
            return BadRequest("دانش آموز یافت نشد !");
        }

        var results = new List<ScoreResult>();
        foreach (var item in result.Scores)
        {
            results.Add(new ScoreResult
            {
                ClassId = item.ClassId,
                ClassName = item.Class.ClassName,
                StudentId = result.Id,
                StudentName = result.FirstName + " " + result.LastName,
                StudentScore = item.StudentScore
            });
        }
        return Ok(results);
    }

    [HttpPost]
    public IActionResult AddScore(int StudentId, int ClassId, string StudentScore)
    {
        try
        {
            db.Scores_tbl.Add(new Score
            {
                ClassId = ClassId,
                StudentId = StudentId,
                StudentScore = StudentScore
            });
            db.SaveChanges();
            return Ok();
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateException){
            return BadRequest("اطلاعات وارده صحیح نمیباشد");
        }
    }
}
