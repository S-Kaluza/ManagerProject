using Application.General;
using Application.Enums;
using Application.General;
using Microsoft.AspNetCore.Identity;

namespace Application.Models.Entity;

public class User : IdentityUser<int>
{
    public string NormalizedEmail { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime CreationDate { get; set; }
    public Status Status { get; set; }
    public int StatusId { get; set; }
    public int RolesId { get; set; }
    public Role Roles { get; set; }
    public Team? Team { get; set; }
    public int? TeamId { get; set; }
    public Company? Company { get; set; }
    public int? CompanyId { get; set; }
}