using Microsoft.EntityFrameworkCore;
using sampleapi.Models;

namespace sampleapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<SampleModel> detail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SampleModel>()
                .ToTable("details");
        }


    }
}
