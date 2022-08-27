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

        public DbSet<Post> Posts { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Post>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Posts)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}