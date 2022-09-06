namespace Befer.Server.Features.Likes
{
    using Befer.Server.Data.Models;

    public interface ILikeService
    {
        public Task<bool> Like(string postId,string userId);

        public Task<bool> Dislike(string postId,string userId);
    }
}
