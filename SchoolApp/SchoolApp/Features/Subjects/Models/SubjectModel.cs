namespace SchoolApp.Features.Subjects.Models;

public class SubjectModel: Base.Model
{
    public string Nume { get; set; }
    public string ProfessorMail { get; set; }
    public List<Double> Grades { get; set; }
}