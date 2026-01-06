using Application.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration.UserConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.CompanyId);
        builder.HasOne(x => x.Team)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.TeamId);
        builder.HasOne(x => x.Roles)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RolesId);
        builder.HasOne(x => x.Status)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.StatusId);
    }
}