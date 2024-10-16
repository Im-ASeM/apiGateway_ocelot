using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Action]")]
public class ClassController:Controller
{
    private readonly Context db;
    public ClassController(Context _db)
    {
        db = _db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Class>> GetClass(){
        return Ok(db.Classes_tbl.ToList());
    }

    [HttpPost]
    public IActionResult AddClass(string ClassName){
        db.Classes_tbl.Add(new Class {
            ClassName = ClassName
        });
        db.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult RemoveClass(int ClassId){
        var check = db.Classes_tbl.Find(ClassId);
        if (check != null){
            db.Classes_tbl.Remove(check);
            db.SaveChanges();
            return Ok("با موفقیت حذف شد");
        }
        return BadRequest("کلاس مورد نظر پیدا نشد");
    }

    
}

