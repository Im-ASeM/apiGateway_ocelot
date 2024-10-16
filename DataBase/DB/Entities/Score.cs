using System.ComponentModel.DataAnnotations;

public class Score
{
    [Key]
    public int Id { get; set; }
    public string StudentScore { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
}