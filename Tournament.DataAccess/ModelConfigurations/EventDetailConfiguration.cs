using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.DataAccess.Models;

namespace Tournament.DataAccess.ModelConfigurations
{
    public class EventDetailConfiguration : IEntityTypeConfiguration<EventDetail>
    {
        public void Configure(EntityTypeBuilder<EventDetail> entity)
        {
            entity.HasKey(e => e.EventDetailId);

            entity.Property(e => e.EventId)
                .HasColumnName("FK_EventId");

            entity.Property(e => e.EventDetailStatusId)
                .HasColumnName("FK_EventDetailStatusId");

            entity.Property(e => e.EventDetailName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.EventDetailNumber);

            entity.Property(e => e.EventDetailOdd)
                .HasColumnType("decimal(38, 7)");

            entity.Property(e => e.FinishingPosition);
            ;

            entity.Property(e => e.FirstTimer);

            entity.HasOne(e => e.EventDetailStatus)
                .WithMany(t => t.EventDetails)
                .HasForeignKey(e => e.EventDetailStatusId)
                .HasConstraintName("FK_EventDetailStatusId");
        }
    }
}