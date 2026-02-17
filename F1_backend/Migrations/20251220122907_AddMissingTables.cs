using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f1api.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverFanRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    TotalVotes = table.Column<int>(type: "int", nullable: false),
                    RatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverFanRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverFanRatings_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamFanRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TotalVotes = table.Column<int>(type: "int", nullable: false),
                    RatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamFanRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamFanRatings_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNftRewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    NFTHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NftImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwardedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNftRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNftRewards_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserNftRewards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTeamVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeamVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTeamVotes_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverFanRatings_DriverId",
                table: "DriverFanRatings",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamFanRatings_TeamId",
                table: "TeamFanRatings",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNftRewards_RaceId",
                table: "UserNftRewards",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNftRewards_UserId",
                table: "UserNftRewards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamVotes_TeamId",
                table: "UserTeamVotes",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamVotes_UserId",
                table: "UserTeamVotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverFanRatings");

            migrationBuilder.DropTable(
                name: "TeamFanRatings");

            migrationBuilder.DropTable(
                name: "UserNftRewards");

            migrationBuilder.DropTable(
                name: "UserTeamVotes");
        }
    }
}
