namespace Befer.Server.Features.Posts
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.Posts.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
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

            this.data.Posts.Add(post);

            await this.data.SaveChangesAsync();

            return post.Id;
        }

        public async Task<bool> Update(UpdatePostRequestModel model, string userId, string id)
        {
            var post = await this.GetByIdAndByUserId(id, userId);

            if (post == null)
            {
                return false;
            }

            post.Description = model.Description;
            post.AfterImgUrl = model.AfterImgUrl;
            post.BeforeImgUrl = model.BeforeImgUrl;
            post.Title = model.Title;
            post.IsPublic = model.IsPublic;
            post.UpdatedAt = DateTime.Now;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(string id, string userId)
        {
            var post = await this.GetByIdAndByUserId(id, userId);

            if (post == null)
            {
                return false;
            }

            await this.data
                .Likes
                .Where(l => l.ToPostId == post.Id)
                .ForEachAsync(l => this.data.Likes.Remove(l));

            //TODO add remove comments by post

            this.data.Posts.Remove(post);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<PostDetailsServiceModel> Details(string id, string userId)
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
                        },
                        IsLiked = p.Likes.Any(l => l.FromUserId == userId)
                    })
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<PostListingServiceModel>> GetAll(string order, int skip)
        {
            //TODO check the order and order the posts according to the provided type

            return await this.data
                .Posts
                .Where(p => p.IsPublic == true)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(PostsPerPage)
                .Select(p => new PostListingServiceModel
                {
                    ObjectId = p.Id,
                    Title = p.Title,
                    BeforeImgUrl = p.BeforeImgUrl,
                    CreatedAt = p.CreatedAt,
                    IsPublic = p.IsPublic
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PostListingServiceModel>> GetMine(string order, int skip, string userId)
        {
            //TODO check the order and order the posts according to the provided type

            return await this.data
                .Posts
                .Where(p => p.Owner.Id == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(PostsPerPage)
                .Select(p => new PostListingServiceModel
                {
                    ObjectId = p.Id,
                    Title = p.Title,
                    BeforeImgUrl = p.BeforeImgUrl,
                    CreatedAt = p.CreatedAt,
                    IsPublic = p.IsPublic
                })
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

        private async Task<Post> GetByIdAndByUserId(string id, string userId)
            => await this.data
                            .Posts
                            .Where(p => p.Id == id && p.OwnerId == userId)
                            .FirstOrDefaultAsync();
    }
}
