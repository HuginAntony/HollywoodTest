using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.DataAccess.Models;

namespace Tournament.DataAccess.ModelConfigurations
{
    public class EventDetailStatusConfiguration : IEntityTypeConfiguration<EventDetailStatus>
    {
        public void Configure(EntityTypeBuilder<EventDetailStatus> entity)
        {
            entity.HasKey(e => e.EventDetailStatusId);

            entity.Property(e => e.EventDetailStatusName)
                .HasConversion<string>();
        }
    }
}