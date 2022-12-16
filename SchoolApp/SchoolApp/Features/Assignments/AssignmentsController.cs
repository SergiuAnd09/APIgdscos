using Microsoft.AspNetCore.Mvc;
using SchoolApp.Features.Assignments.Models;
using SchoolApp.Features.Assignments.Views;

namespace SchoolApp.Features.Assignments;

[ApiController]
[Route("assignments")]

public class AssignmentsController
{
    private static List<AssignmentModel> _mockDb = new List<AssignmentModel>(); //lista merge ca baza de date mock, si variabilele cu underscore sunt pt db

    public AssignmentsController()
    {
    }
//metoda de cautare dupa ID
//avem o functie in liste , ii dam un route nou

    [HttpPost] //adauga informatii in baza de date
    public AssignmentResponse Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,              //asta e maparea de mana 
            Description = request.Description,
            DeadLine = request.Deadline
        };
        
        _mockDb.Add(assignment);

        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }

    [HttpGet]
    public IEnumerable<AssignmentResponse> Get()
    {
        return _mockDb.Select(
            assignment => new AssignmentResponse
            {
                Id = assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine
            }
        ).ToList();
    }

    [HttpGet("{id}")]
    public AssignmentResponse Get([FromRoute] string id)
    {
        var assignment = _mockDb.FirstOrDefault(x => x.Id == id);

        if (assignment is null)
        {
            return null;
        }

        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }
}

//tema e sa facem o functie de delete si update, nu punem routeuri, [HttpDelete] si [HttpPatch]
//delete cauta o var, testeaza, o sterge si intoarce raspunsul var sters
//pudate primeste un strinng si un request cu noile date de update. gasesc var, iau fiecare property si il actualizez si dau return la obicetul actualizat sun forma de response
