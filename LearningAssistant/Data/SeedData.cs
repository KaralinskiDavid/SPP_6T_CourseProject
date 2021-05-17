using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAssistant.Data
{
    public static class SeedData
    {
        public async static Task EnsurePopulated(IApplicationBuilder app)
        {
            LearningAssistantIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<LearningAssistantIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();

            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(new List<IdentityRole>
                {
                    new IdentityRole{Name="Admin", NormalizedName="ADMIN"},
                    new IdentityRole{Name="SpecialityHeadman", NormalizedName="SPECIALITYHEADMAN"},
                    new IdentityRole{Name="GroupHeadman", NormalizedName="GROUPHEADMAN"},
                    new IdentityRole{Name="Student", NormalizedName="STUDENT"}
                });
                await context.SaveChangesAsync();
            }

        }
    }
}
