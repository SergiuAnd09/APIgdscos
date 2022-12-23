using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Features.Tests.Views;

public class TestRequest
{
    [Required] public string Subject { get; set; }
    [Required] public DateTime TestDate { get; set; }
}