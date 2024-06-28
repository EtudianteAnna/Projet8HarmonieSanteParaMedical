using Microsoft.EntityFrameworkCore;
using CommonModels;

namespace PraticiensApi.Data
{
    public class PraticienDb : DbContext
    {
        public PraticienDb(DbContextOptions<PraticienDb> options) : base(options)
        {
        }

        public DbSet<PraticienCalendars> Praticiens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurations supplémentaires
        }
    }
}