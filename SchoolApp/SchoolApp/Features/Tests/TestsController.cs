using Microsoft.AspNetCore.Mvc;
using SchoolApp.Features.Tests.Models;
using SchoolApp.Features.Tests.Views;

namespace SchoolApp.Features.Tests;

[ApiController]
[Route("tests")]

public class TestsController
{
    private static List<TestModel> _mockDb = new List<TestModel>(); //lista merge ca baza de date mock, si variabilele cu underscore sunt pt db

    public TestsController()
    {
    }
//metoda de cautare dupa ID
//avem o functie in liste , ii dam un route nou

    [HttpPost] //adauga informatii in baza de date
    public TestResponse Add(TestRequest request)
    {
        var test = new TestModel()
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,              //asta e maparea de mana 
            TestDate = request.TestDate
        };
        
        _mockDb.Add(test);

        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpGet]
    public IEnumerable<TestResponse> Get()
    {
        return _mockDb.Select(
            test => new TestResponse()
            {
                Id = test.Id,
                Subject = test.Subject,
                TestDate = test.TestDate
            }
        ).ToList();
    }

    [HttpGet("{id}")]
    public TestResponse Get([FromRoute] string id)
    {
        var test = _mockDb.FirstOrDefault(x => x.Id == id);

        if (test is null)
        {
            return null;
        }

        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }


    //[HttpDelete]
    
    [HttpDelete("{id}")]
    public TestResponse Delete([FromRoute] string id)
    {
        var test = _mockDb.FirstOrDefault(x => x.Id == id);

        if (test is null)
        {
            return null;
        }

        _mockDb.Remove(test);
        
        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    //[HttpPatch]
    
    [HttpPatch("{id}")]
    public TestResponse Patch([FromRoute] string id,[FromBody] TestRequest nou)
    {
        var test = _mockDb.FirstOrDefault(x => x.Id == id);

        if (test is null)
        {
            return null;
        }

        int i = _mockDb.IndexOf(test);
        
        _mockDb[i].Subject = nou.Subject;
        _mockDb[i].TestDate = nou.TestDate; 
        _mockDb[i].Updated = DateTime.UtcNow;

        return new TestResponse()
        {
            Id = id,
            Subject = nou.Subject,
            TestDate = nou.TestDate
        };
    }
}

//tema e sa facem o functie de delete si update, nu punem routeuri, [HttpDelete] si [HttpPatch]
//delete cauta o var, testeaza, o sterge si intoarce raspunsul var sters
//pudate primeste un strinng si un request cu noile date de update. gasesc var, iau fiecare property si il actualizez si dau return la obicetul actualizat sun forma de response
