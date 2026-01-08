using Application.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Application.Models.Entity.Task;

namespace DataAccess.Configuration.TaskConfiguration;

public class TasksStatusesConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Users)
            .WithMany(x => x.Tasks);
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Tasks);
        builder.HasOne(x => x.TasksStatus)
            .WithMany(x => x.Tasks);
        builder.HasOne(x => x.Team)
            .WithMany(x => x.Tasks);
    }
}