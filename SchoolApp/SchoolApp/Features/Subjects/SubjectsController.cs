using Microsoft.AspNetCore.Mvc;
using SchoolApp.Features.Subjects.Models;
using SchoolApp.Features.Subjects.Views;

namespace SchoolApp.Features.Subjects;

[ApiController]
[Route("subjects")]

public class SubjectsController
{
    private static List<SubjectModel> _mockDb = new List<SubjectModel>(); //lista merge ca baza de date mock, si variabilele cu underscore sunt pt db

    public SubjectsController()
    {
    }
//metoda de cautare dupa ID
//avem o functie in liste , ii dam un route nou

    [HttpPost] //adauga informatii in baza de date
    public SubjectResponse Add(SubjectRequest request)
    {
        var subject = new SubjectModel()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Nume = request.Nume,
            ProfessorMail = request.ProfessorMail,
            Grades = request.Grades
        };
        
        _mockDb.Add(subject);

        return new SubjectResponse()
        {
            Id = subject.Id,
            Nume = subject.Nume,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    [HttpGet]
    public IEnumerable<SubjectResponse> Get()
    {
        return _mockDb.Select(
            subject => new SubjectResponse()
            {
                Id = subject.Id,
                Nume = subject.Nume,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades
            }
        ).ToList();
    }

    [HttpGet("{id}")]
    public SubjectResponse Get([FromRoute] string id)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);

        if (subject is null)
        {
            return null;
        }

        return new SubjectResponse()
        {
            Id = subject.Id,
            Nume = subject.Nume,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }


    //[HttpDelete]
    
    [HttpDelete("{id}")]
    public SubjectResponse Delete([FromRoute] string id)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);

        if (subject is null)
        {
            return null;
        }

        _mockDb.Remove(subject);
        
        return new SubjectResponse()
        {
            Id = subject.Id,
            Nume = subject.Nume,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades
        };
    }

    //[HttpPatch]
    
    [HttpPatch("{id}")]
    public SubjectResponse Patch([FromRoute] string id,[FromBody] SubjectRequest n)
    {
        var subject = _mockDb.FirstOrDefault(x => x.Id == id);

        if (subject is null)
        {
            return null;
        }

        int i = _mockDb.IndexOf(subject);
        
        _mockDb[i].Nume = n.Nume;
        _mockDb[i].ProfessorMail = n.ProfessorMail;
        _mockDb[i].Grades = n.Grades;
        _mockDb[i].Updated = DateTime.UtcNow;

        return new SubjectResponse()
        {
            Id = id,
            Nume = n.Nume,
            ProfessorMail = n.ProfessorMail,
            Grades = n.Grades
        };
    }
}

//tema e sa facem o functie de delete si update, nu punem routeuri, [HttpDelete] si [HttpPatch]
//delete cauta o var, testeaza, o sterge si intoarce raspunsul var sters
//pudate primeste un strinng si un request cu noile date de update. gasesc var, iau fiecare property si il actualizez si dau return la obicetul actualizat sun forma de response
