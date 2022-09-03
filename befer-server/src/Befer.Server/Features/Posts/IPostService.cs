namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data.Models;

    public interface IPostService
    {
        public Task<string> Create(
            string title,
            string afterImgUrl,
            string beforeImgUrl,
            string description,
            bool isPublic,
            string userId);

        public Task<GetPostResponseModel> Get(string id);

        public Task<int> AllPostsCount();

        public Task<int> UserPostsCount(string userId);
    }
}
