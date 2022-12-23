using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Subjects.Views;

public class SubjectRequest
{
    [Required] public string Nume { get; set; }
    [Required] public string ProfessorMail { get; set; }
    [Required] public List<Double> Grades { get; set; }
}