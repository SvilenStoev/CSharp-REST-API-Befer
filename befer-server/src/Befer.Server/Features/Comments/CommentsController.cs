namespace Befer.Server.Features.Comments
{
    using Befer.Server.Features.Comments.Models;
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCommentRequestModel model)
        {
            var userId = User.GetId();

            var created = await this.commentService.Create(model.Content, model.PostId, userId);

            if (!created)
            {
                return BadRequest();
            }

            return Created(nameof(Create), new { Created = true });
        }

        [HttpGet]
        [Route(postId)]
        public async Task<IEnumerable<CommentListingServiceModel>> GetAll([FromRoute] string postId)
        {
            return await this.commentService.GetAll(postId);
        }

        [HttpPut]
        [Route(commentId)]
        public async Task<ActionResult> Update([FromRoute] string commentId, [FromBody] UpdateCommentRequestModel model)
        {
            var userId = User.GetId();

            var updated = await this.commentService.Update(model.Content, commentId, userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(commentId)]
        public async Task<ActionResult> Delete(string commentId)
        {
            var userId = User.GetId();

            var deleted = await this.commentService.Delete(commentId, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
