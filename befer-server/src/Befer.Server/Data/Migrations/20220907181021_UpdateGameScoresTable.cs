using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Befer.Server.Data.Migrations
{
    public partial class UpdateGameScoresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AliensMissed",
                table: "GameScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HealthRemaining",
                table: "GameScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliensMissed",
                table: "GameScores");

            migrationBuilder.DropColumn(
                name: "HealthRemaining",
                table: "GameScores");
        }
    }
}
