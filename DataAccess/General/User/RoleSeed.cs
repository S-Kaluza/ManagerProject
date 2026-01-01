using Application.General;
using Application.Enums;

namespace DataAccess.General.User;

public class RoleSeed
{
    public static List<Role> Seed()
    {
        List<Role> rolesList = new();
        foreach (RolesEnum item in Enum.GetValues(typeof(RolesEnum)))
            rolesList.Add(new Role
            {
                Id = (int)item,
                Name = item.ToString()
            });
        return rolesList;
    }
}