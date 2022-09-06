namespace Befer.Server.Data
{
    using Befer.Server.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Emit;

    public class BeferDbContext : IdentityDbContext<User>
    {
        public BeferDbContext(DbContextOptions<BeferDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; init; }

        public DbSet<Like> Likes { get; init; }

        public DbSet<Comment> Comments { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Post>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Posts)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Like>()
                .HasKey(l => new { l.FromUserId, l.ToPostId });

            builder
                .Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ToPostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Like>()
               .HasOne(l => l.User)
               .WithMany(u => u.GivenLikes)
               .HasForeignKey(l => l.FromUserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}