namespace Befer.Server.Features.Comments
{
    public interface ICommentService
    {
        public Task<bool> Create(string content, string postId, string userId);
    }
}
