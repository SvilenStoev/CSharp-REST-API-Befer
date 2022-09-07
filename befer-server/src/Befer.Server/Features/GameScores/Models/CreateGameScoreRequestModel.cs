namespace Befer.Server.Features.GameScores.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGameScoreRequestModel
    {
        [Required]
        [Range(0, 1000)]
        public int AliensKilled { get; set; }

        [Required]
        [Range(-1, 500)]
        public int TimeRemaining { get; set; }

        [Required]
        [Range(0, 101)]
        public int BoostRemaining { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int TotalPoints { get; set; }
    }
}
