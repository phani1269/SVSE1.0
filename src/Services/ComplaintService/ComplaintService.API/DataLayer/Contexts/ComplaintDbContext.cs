using ComplaintService.API.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.API.DataLayer.Contexts
{
    public class ComplaintDbContext : DbContext
    {
        public ComplaintDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ComplaintModel> complaints { get; set; }
    }
}
