using Application.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration.TeamConfiguration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Teams)
            .HasForeignKey(x => x.CompanyId);
        builder.HasMany(x => x.Users)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId);
        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId);
        
    }
}