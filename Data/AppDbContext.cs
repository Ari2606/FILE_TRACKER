using Microsoft.EntityFrameworkCore;
using Rec_Tracker.Models;

namespace Rec_Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Rec_Details> Record_details { get; set; }

    }
}