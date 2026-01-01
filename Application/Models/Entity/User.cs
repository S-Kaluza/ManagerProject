using Application.General;
using Application.Enums;
using Application.General;
using Microsoft.AspNetCore.Identity;

namespace Application.Models.Entity;

public class User : IdentityUser<int>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime CreationDate { get; set; }
    public StatusEnum Status { get; set; }
    public int StatusId { get; set; }
    public int RolesId { get; set; }
    public Role Roles { get; set; }
}