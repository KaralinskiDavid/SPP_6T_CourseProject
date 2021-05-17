using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dto.Identity;

namespace LearningAssistant.Data
{
    public class LearningAssistantIdentityDbContext : IdentityDbContext<LearningAssistantUser>
    {
        public LearningAssistantIdentityDbContext(DbContextOptions opts) : base(opts) { }
    }
}
