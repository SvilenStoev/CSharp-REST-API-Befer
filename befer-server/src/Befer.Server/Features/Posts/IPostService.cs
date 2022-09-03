namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Posts.Models;

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

        public Task<IEnumerable<PostListResponseModel>> GetAll(string order, int skip);

        public Task<IEnumerable<PostListResponseModel>> GetMine(string order, int skip, string userId);

        public Task<int> AllPostsCount();

        public Task<int> UserPostsCount(string userId);
    }
}
