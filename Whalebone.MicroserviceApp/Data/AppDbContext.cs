using Microsoft.EntityFrameworkCore;
using Whalebone.MicroserviceApp.Models;

namespace Whalebone.MicroserviceApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
}
