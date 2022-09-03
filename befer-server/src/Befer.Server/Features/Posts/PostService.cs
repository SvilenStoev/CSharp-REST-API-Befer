namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Posts.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using static FeaturesConstants.Post;

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

        public async Task<PostDetailsServiceModel> Details(string id)
            => await this.data
                    .Posts
                    .Where(p => p.Id == id)
                    .Select(p => new PostDetailsServiceModel
                    {
                        ObjectId = p.Id,
                        Title = p.Title,
                        AfterImgUrl = p.AfterImgUrl,
                        BeforeImgUrl = p.BeforeImgUrl,
                        Description = p.Description,
                        IsPublic = p.IsPublic,
                        OwnerId = p.Owner.Id,
                        Owner = new PostOwnerDetailsServiceModel
                        {
                            ObjectId = p.Owner.Id,
                            Email = p.Owner.Email,
                            FullName = p.Owner.FullName,
                            Username = p.Owner.UserName
                        }
                    })
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<PostListingServiceModel>> GetAll(string order, int skip)
        {
            return await this.data
                .Posts
                .Where(p => p.IsPublic == true)
                .Select(p => new PostListingServiceModel
                {
                    ObjectId = p.Id,
                    Title = p.Title,
                    BeforeImgUrl = p.BeforeImgUrl,
                    CreatedAt = p.CreatedAt,
                    IsPublic = p.IsPublic
                })
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(PostsPerPage)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostListingServiceModel>> GetMine(string order, int skip, string userId)
        {
            return await this.data
                .Posts
                .Where(p => p.Owner.Id == userId)
                .Select(p => new PostListingServiceModel
                {
                    ObjectId = p.Id,
                    Title = p.Title,
                    BeforeImgUrl = p.BeforeImgUrl,
                    CreatedAt = p.CreatedAt,
                    IsPublic = p.IsPublic
                })
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(PostsPerPage)
                .ToListAsync();
        }

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
