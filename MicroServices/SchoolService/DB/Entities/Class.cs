using System.ComponentModel.DataAnnotations;

public class Class
{
    [Key]
    public int Id { get; set; }
    public string ClassName { get; set; }
}