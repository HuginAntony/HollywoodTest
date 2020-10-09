using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.DataAccess.Models;

namespace Tournament.DataAccess.ModelConfigurations
{
    
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasKey(e => e.EventId);

            entity.Property(e => e.TournamentId)
                .HasColumnName("FK_TournamentId");

            entity.Property(e => e.EventName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.EventNumber);

            entity.Property(e => e.EventDateTime)
                .HasColumnType("datetime");

            entity.Property(e => e.EventEndDateTime)
                .HasColumnType("datetime");

            entity.Property(e => e.AutoClose);

            entity.HasOne(e => e.Tournament)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TournamentId)
                .HasConstraintName("FK_Event_Tournament");

            entity.HasMany(e => e.EventDetails)
                .WithOne(p => p.Event)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_Event_EventDetails");
        }
    }
}
