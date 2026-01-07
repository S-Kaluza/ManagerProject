using Application.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Application.Models.Entity.Task;

namespace DataAccess.Configuration.TaskConfiguration;

public class TasksStatusesConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable("TasksStatuses");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasMany(x => x.Users)
            .WithMany(x => x.Tasks);
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Tasks);
    }
}