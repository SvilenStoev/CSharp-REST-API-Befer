namespace Befer.Server.Features.GameScores
{
    using Befer.Server.Features.GameScores.Models;
    using Befer.Server.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class GameScoresController : ApiController
    {
        private readonly IGameScoreService gameScoreService;

        public GameScoresController(IGameScoreService gameScoreService)
        {
            this.gameScoreService = gameScoreService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGameScoreRequestModel model)
        {
            var userId = User.GetId();

            var created = await this.gameScoreService.Create(model, userId);

            if (!created)
            {
                return BadRequest();
            }

            return Created(nameof(Create), new { Created = true });
        }

        [HttpPut]
        [Route("{scoresId}")]
        public async Task<ActionResult> Update([FromBody] UpdateGameScoreRequestModel model, [FromRoute] string scoresId)
        {
            var userId = User.GetId();

            var updated = await this.gameScoreService.Update(model, userId, scoresId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route(nameof(GetMine))]
        public async Task<IEnumerable<GameScoresServiceModel>> GetMine()
        {
            var userId = User.GetId();

            return await this.gameScoreService.GetMine(userId);
        }

        [HttpGet]
        [Route(nameof(GetALl))]
        public async Task<IEnumerable<GameScoresAllServiceModel>> GetALl()
            => await this.gameScoreService.GetAll();
    }
}
