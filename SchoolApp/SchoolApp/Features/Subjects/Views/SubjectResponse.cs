namespace SchoolApp.Features.Subjects.Views;

public class SubjectResponse
{
    public string Id { get; set; }
    public string Nume { get; set; }
    public string ProfessorMail { get; set; }
    public List<Double> Grades { get; set; }
}