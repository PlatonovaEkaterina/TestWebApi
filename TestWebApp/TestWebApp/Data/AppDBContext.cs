using Microsoft.EntityFrameworkCore;
using TestWebApp.Data.Models;

namespace TestWebApp;

public class AppDBContext: DbContext
{
    public DbSet<Client> Clients { get; set; }
    
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    
}