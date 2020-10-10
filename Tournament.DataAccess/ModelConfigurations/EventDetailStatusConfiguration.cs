using System.Collections.Generic;
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
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasData(new List<EventDetailStatus>
            {
                new EventDetailStatus{EventDetailStatusId = 1, EventDetailStatusName = EventDetailStatusNames.Active },
                new EventDetailStatus{EventDetailStatusId = 2, EventDetailStatusName = EventDetailStatusNames.Scratched },
                new EventDetailStatus{EventDetailStatusId = 3, EventDetailStatusName = EventDetailStatusNames.Closed }
            });
        }
    }
}