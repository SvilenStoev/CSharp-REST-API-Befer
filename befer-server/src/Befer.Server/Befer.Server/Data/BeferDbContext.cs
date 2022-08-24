namespace Befer.Server.Data
{
    using Befer.Server.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BeferDbContext : IdentityDbContext<User>
    {
        public BeferDbContext(DbContextOptions<BeferDbContext> options)
            : base(options)
        {
        }
    }
}