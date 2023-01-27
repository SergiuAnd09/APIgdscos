using Microsoft.EntityFrameworkCore;
using SchoolApp.Features.Assignments.Models;

namespace SchoolApp.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}
    public DbSet<AssignmentModel> Assignments { get; set; }
}