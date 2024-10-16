using System.ComponentModel.DataAnnotations;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CodeMeli { get; set; }
    public string Phone { get; set; }
    public ICollection<Score> Scores { get; set; }
    public ICollection<ClassStudent> ClassStudents { get; set; }
}