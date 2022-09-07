namespace Befer.Server.Features.GameScores.Models
{
    public class GameScoresAllServiceModel
    {
        public string Username { get; set; }

        public int TotalPoints { get; set; }

        public int AliensKilled { get; set; }

        public int AliensMissed { get; set; }

        public int HealthRemaining { get; set; }

        public int TimeRemaining { get; set; }

        public int BoostRemaining { get; set; }

        public int Points { get; set; }

    }
}
