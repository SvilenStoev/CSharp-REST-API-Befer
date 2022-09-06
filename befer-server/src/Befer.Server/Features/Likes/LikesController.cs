namespace Befer.Server.Features.Likes
{
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class LikesController : ApiController
    {
        private readonly ILikeService likeService;

        public LikesController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpPost]
        [Route("{postId}")]
        public async Task<ActionResult> Like([FromRoute] string postId)
        {
            var userId = User.GetId();

            var liked = await this.likeService.Like(postId, userId);

            if (!liked)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{postId}")]
        public async Task<ActionResult> Dislike([FromRoute] string postId)
        {
            var userId = User.GetId();

            var disliked = await this.likeService.Dislike(postId, userId);

            if (!disliked)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
