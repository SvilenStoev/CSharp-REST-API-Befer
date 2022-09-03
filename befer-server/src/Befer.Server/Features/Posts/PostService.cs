namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class PostService : IPostService
    {
        private readonly BeferDbContext data;

        public PostService(BeferDbContext data) => this.data = data;

        public async Task<string> Create(
            string title,
            string afterImgUrl,
            string beforeImgUrl,
            string description,
            bool isPublic,
            string userId)
        {
            var post = new Post
            {
                Title = title,
                AfterImgUrl = afterImgUrl,
                BeforeImgUrl = beforeImgUrl,
                Description = description,
                IsPublic = isPublic,
                OwnerId = userId
            };

            data.Posts.Add(post);

            await data.SaveChangesAsync();

            return post.Id;
        }

        public async Task<GetPostResponseModel> Get(string id)
            => await this.data
                    .Posts
                    .Where(p => p.Id == id)
                    .Select(p => new GetPostResponseModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        AfterImgUrl = p.AfterImgUrl,
                        BeforeImgUrl = p.BeforeImgUrl,
                        Description = p.Description,
                        IsPublic = p.IsPublic,
                        OwnerId = p.Owner.Id,
                        Owner = new PostOwnerResponseModel
                        {
                            ObjectId = p.Owner.Id,
                            Email = p.Owner.Email,
                            FullName = p.Owner.FullName,
                            Username = p.Owner.UserName
                        }
                    })
                    .FirstOrDefaultAsync();

        public async Task<int> AllPostsCount()
         => await this.data
                .Posts
                .Where(p => p.IsPublic == true)
                .CountAsync();

        public async Task<int> UserPostsCount(string userId)
          => await this.data
                 .Posts
                 .Where(p => p.OwnerId == userId)
                 .CountAsync();
    }
}
