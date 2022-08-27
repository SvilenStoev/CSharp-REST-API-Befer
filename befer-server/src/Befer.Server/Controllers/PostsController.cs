namespace Befer.Server.Controllers
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Infrastructure;
    using Befer.Server.Models.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        private readonly BeferDbContext data;

        public PostsController(BeferDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(CreatePostRequestModel model)
        {
            var userId = this.User.GetId();

            var post = new Post
            {
                Title = model.Title,
                AfterImgUrl = model.AfterImgUrl,
                BeforeImgUrl = model.BeforeImgUrl,
                Description = model.Description,
                IsPublic = model.IsPublic,
                OwnerId = userId
            };

            this.data.Posts.Add(post);

            await this.data.SaveChangesAsync();

            return Created(nameof(this.Create), post.Id);
        }
    }
}
