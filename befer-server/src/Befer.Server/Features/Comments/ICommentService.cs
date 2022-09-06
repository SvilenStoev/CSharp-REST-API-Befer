namespace Befer.Server.Features.Comments
{
    using Befer.Server.Features.Comments.Models;

    public interface ICommentService
    {
        public Task<bool> Create(string content, string postId, string userId);

        public Task<IEnumerable<CommentListingServiceModel>> GetAll(string postId);

        public Task<bool> Update(string content, string commentId, string userId);

        public Task<bool> Delete(string commentId, string userId);
    }
}
