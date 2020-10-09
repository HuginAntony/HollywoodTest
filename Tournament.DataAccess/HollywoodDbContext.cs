using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.DataAccess.ModelConfigurations;
using Tournament.DataAccess.Models;

namespace Tournament.DataAccess
{
    public class HollywoodDbContext : IdentityDbContext<ApplicationUser>
    {
        public HollywoodDbContext(DbContextOptions<HollywoodDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Models.Tournament> Tournament { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventDetail> EventDetail { get; set; }
        public virtual DbSet<EventDetailStatus> EventDetailStatus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventDetailConfiguration());
            modelBuilder.ApplyConfiguration(new EventDetailStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}