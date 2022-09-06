namespace Befer.Server.Features.Likes
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class LikeService : ILikeService
    {
        private readonly BeferDbContext data;

        public LikeService(BeferDbContext data) => this.data = data;

        public async Task<bool> Like(string postId, string userId)
        {
            var post = await this.data
                .Posts
                .Where(p => p.Id == postId && p.Likes.All(l => l.FromUserId != userId))
                .FirstOrDefaultAsync();

            if (post == null || post.OwnerId == userId)
            {
                return false;
            }

            var like = new Like
            {
                FromUserId = userId,
                ToPostId = postId
            };

            this.data.Likes.Add(like);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Dislike(string postId, string userId)
        {
            var post = await this.data
                .Posts
                .Where(p => p.Id == postId)
                .FirstOrDefaultAsync();

            var like = await this.data
                .Likes
                .Where(l => l.FromUserId == userId && l.ToPostId == postId)
                .FirstOrDefaultAsync();

            if (post == null || like == null)
            {
                return false;
            }

            this.data.Likes.Remove(like);

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
