namespace Befer.Server.Features.Comments
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Comments.Models;
    using Befer.Server.Features.Posts.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using static FeaturesConstants.Comment;

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

        public async Task<bool> Update(string content, string commentId, string userId)
        {
            var comment = await this.GetByIdAndByUserId(commentId, userId);

            if (comment == null)
            {
                return false;
            }

            comment.Content = content;
            comment.UpdatedAt = DateTime.Now;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string commentId, string userId)
        {
            var comment = await this.GetByIdAndByUserId(commentId, userId);

            if (comment == null)
            {
                return false;
            }

            this.data.Comments.Remove(comment);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CommentListingServiceModel>> GetAll(string postId)
            => await this.data
                .Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .Take(CommentsPerPost)
                .Select(c => new CommentListingServiceModel
                {
                    ObjectId = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsDeleted = c.IsDeleted,
                    DeletedOn = c.DeletedOn,
                    PostId = c.PostId,
                    Author = new CommentAuthorListingServiceModel
                    {
                        ObjectId = c.AuthorId,
                        Username = c.Author.UserName,
                        FullName = c.Author.FullName,
                        Email = c.Author.Email
                    }
                })
                .ToListAsync();

        private async Task<Comment> GetByIdAndByUserId(string commentId, string userId)
          => await this.data
                          .Comments
                          .Where(c => c.Id == commentId && c.AuthorId == userId)
                          .FirstOrDefaultAsync();
    }
}
