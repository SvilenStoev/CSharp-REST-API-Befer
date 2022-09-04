namespace Befer.Server.Features.Posts
{
    using Befer.Server.Features.Posts.Models;
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class PostsController : ApiController
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePostRequestModel model)
        {
            var userId = User.GetId();

            string postId = await this.postService.Create(
                model.Title,
                model.AfterImgUrl,
                model.BeforeImgUrl,
                model.Description,
                model.IsPublic,
                userId);

            var response = new CreatePostResponseModel
            {
                ObjectId = postId
            };

            return Created(nameof(this.Create), response);
        }

        [HttpPut]
        [Route(Id)]
        public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdatePostRequestModel model)
        {
            var userId = User.GetId();

            var updated = await this.postService.Update(model, userId, id);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(string id)
        {
            var userId = User.GetId();

            var deleted = await this.postService.Delete(id, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult<PostDetailsServiceModel>> Details(string id)
         => await this.postService.Details(id);

        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<PostListingServiceModel>> GetAll(string order, int skip)
        {
            return await this.postService.GetAll(order, skip);
        }


        [HttpGet]
        [Route(nameof(GetMine))]
        public async Task<IEnumerable<PostListingServiceModel>> GetMine(string order, int skip)
        {
            var userId = User.GetId();

            return await this.postService.GetMine(order, skip, userId);
        }

        [HttpGet]
        [Route(nameof(AllPostsCount))]
        public async Task<ActionResult<int>> AllPostsCount()
        {
            return await this.postService.AllPostsCount();
        }

        [HttpGet]
        [Route(nameof(UserPostsCount))]
        public async Task<ActionResult<int>> UserPostsCount()
        {
            var userId = User.GetId();

            return await this.postService.UserPostsCount(userId);
        }
    }
}
