using Application.General;
using Application.Enums;
using Task = Application.Models.Entity.Task;

namespace DataAccess.General.User;

public class TasksStatusSeed
{
    public static List<TasksStatus> Seed()
    {
        List<TasksStatus> tasksStatusList = new();
        foreach (TasksStatusEnum item in Enum.GetValues(typeof(TasksStatusEnum)))
            tasksStatusList.Add(new TasksStatus()
            {
                Id = (int)item,
                Name = item.ToString()
            });
        return tasksStatusList;
    }
}