using Application.General;
using DataAccess.General.User;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.General;

public static class SeedConfiguration
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(StatusSeed.Seed());
        modelBuilder.Entity<Role>().HasData(RoleSeed.Seed());
    }
}