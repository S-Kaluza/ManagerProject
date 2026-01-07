using Application.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration.TasksStatusesConfiguration;

public class TasksStatusesConfiguration : IEntityTypeConfiguration<TasksStatus>
{
    public void Configure(EntityTypeBuilder<TasksStatus> builder)
    {
        builder.ToTable("TasksStatuses");
        builder.HasKey(x => x);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.TasksStatus)
            .HasForeignKey(x => x.TaskStatusId);
    }
}