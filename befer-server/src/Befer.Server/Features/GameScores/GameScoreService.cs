namespace Befer.Server.Features.GameScores
{
    using Befer.Server.Data;
    using Befer.Server.Data.Models;
    using Befer.Server.Features.GameScores.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GameScoreService : IGameScoreService
    {
        private readonly BeferDbContext data;

        public GameScoreService(BeferDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> Create(CreateGameScoreRequestModel model, string userId)
        {
            var gameScore = new GameScore
            {
                AliensKilled = model.AliensKilled,
                AliensMissed = model.AliensMissed,
                HealthRemaining = model.HealthRemaining,
                BoostRemaining = model.BoostRemaining,
                Points = model.Points,
                TimeRemaining = model.TimeRemaining,
                TotalPoints = model.TotalPoints,
                PlayerId = userId,
            };

            await this.data.GameScores.AddAsync(gameScore);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GameScoresServiceModel>> GetMine(string userId)
        {
            var gameScores = await this.data
                .GameScores
                .Where(gs => gs.PlayerId == userId)
                .Select(gs => new GameScoresServiceModel
                {
                    ObjectId = gs.Id,
                    TotalPoints = gs.TotalPoints
                })
                .ToListAsync();

            return gameScores;
        }

        public async Task<IEnumerable<GameScoresAllServiceModel>> GetAll()
        {
            var allGameScores = await this.data
                .GameScores
                .OrderByDescending(gs => gs.TotalPoints)
                .Take(10)
                .Select(gs => new GameScoresAllServiceModel
                {
                    Username = gs.Player.UserName,
                    AliensKilled = gs.AliensKilled,
                    AliensMissed = gs.AliensMissed,
                    TotalPoints = gs.TotalPoints,
                    BoostRemaining = gs.BoostRemaining,
                    HealthRemaining = gs.HealthRemaining,
                    Points = gs.Points,
                    TimeRemaining = gs.TimeRemaining
                })
                .ToListAsync();

            return allGameScores;
        }
    }
}
