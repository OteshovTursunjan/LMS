using LMS.Domain.Common;
using LMS.Domain.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LMS.Shared;
using LMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;


namespace LMS.Infrastructure.Persistance;

public  class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { FirstName = "Tony", LastName = "SS", UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };
            await userManager.CreateAsync(user, "Admin123.?");
        }
        await context.SaveChangesAsync();
    }
}
