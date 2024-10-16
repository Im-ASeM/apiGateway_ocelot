using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Action]")]
public class StudentController:Controller
{
    private readonly Context db;
    public StudentController(Context _db)
    {
        db = _db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudent(){
        return Ok(db.Students_tbl.ToList());
    }

    [HttpPost]
    public IActionResult AddStudent(string FirstName , string LastName , string CodeMeli , string Phone){
        db.Students_tbl.Add(new Student {
            FirstName = FirstName,
            LastName = LastName,
            CodeMeli = CodeMeli,
            Phone = Phone
        });
        db.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult RemoveStudent(int StudentId){
        var check = db.Students_tbl.Find(StudentId);
        if (check != null){
            db.Students_tbl.Remove(check);
            db.SaveChanges();
            return Ok("با موفقیت حذف شد");
        }
        return BadRequest("دانش آموز مورد نظر پیدا نشد");
    }

    
}

