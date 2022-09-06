namespace Befer.Server.Features.Comments
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly BeferDbContext data;

        public CommentService(BeferDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> Create(string content, string postId, string userId)
        {
            var post = await this.data
                .Posts
                .Where(p => p.Id == postId)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return false;
            }

            var comment = new Comment
            {
                AuthorId = userId,
                Content = content,
                PostId = postId
            };

            await this.data.Comments.AddAsync(comment);

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}
