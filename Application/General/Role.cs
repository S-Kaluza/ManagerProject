using Application.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace Application.General;

public class Role : IdentityRole<int>
{
    public IList<User> Users { get; set; }
}