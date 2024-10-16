using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/Class/[Action]")]
public class ClassController:Controller
{
    private readonly Context db;
    public ClassController(Context _db)
    {
        db = _db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetClassmate(int ClassId){
        var result = db.Classes_tbl
            .Where(x=>x.Id == ClassId)
            .Include(x=>x.ClassStudents)
                .ThenInclude(x=>x.Student)
            .SelectMany(x=>x.ClassStudents.Select(y=>y.Student))
            .ToList();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddToClass(int ClassId , int StudentId){
        var ClassCheck = db.Classes_tbl.Find(ClassId);
        var StudentCheck = db.Students_tbl.Find(StudentId);

        if(ClassCheck == null){
            return BadRequest("کلاس مورد نظر یافت نشد");
        }
        else if (StudentCheck == null){
            return BadRequest("دانش آموز مورد نظر یافت نشد");
        }
        else if (db.ClassStudents.Any(x=>x.ClassId == ClassId && x.StudentId == StudentId)){
            return BadRequest("دانش آموز مورد نظر در حال حاضر عضو کلاس میباشد");
        }
        db.ClassStudents.Add(new ClassStudent{
            ClassId = ClassId,
            StudentId = StudentId
        });
        db.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult RemoveFromClass(int ClassId , int StudentId){
        var check = db.ClassStudents.FirstOrDefault(x=>x.ClassId == ClassId && x.StudentId == StudentId);
        if (check != null){
            db.ClassStudents.Remove(check);
            db.SaveChanges();
            return Ok("با موفقیت حذف شد");
        }
        return BadRequest("مجددا بررسی کنید . حذف موفقیت امیز نبود");
    }

    
}

