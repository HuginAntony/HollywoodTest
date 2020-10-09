using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.DataAccess.ModelConfigurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Models.Tournament>
    {
        public void Configure(EntityTypeBuilder<Models.Tournament> entity)
        {
            entity.HasKey(e => e.TournamentId);

            entity.Property(e => e.TournamentName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}