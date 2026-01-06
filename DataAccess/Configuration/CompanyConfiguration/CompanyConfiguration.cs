using Application.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration.CompanyConfiguration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Teams)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId);
        builder.HasMany(x => x.Users)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId);
    }
}