using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f1api.Migrations
{
    /// <inheritdoc />
    public partial class VoteTransactionAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RacePrediction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    SecondId = table.Column<int>(type: "int", nullable: false),
                    ThirdId = table.Column<int>(type: "int", nullable: false),
                    PredictionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacePrediction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacePrediction_Drivers_SecondId",
                        column: x => x.SecondId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacePrediction_Drivers_ThirdId",
                        column: x => x.ThirdId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacePrediction_Drivers_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacePrediction_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RacePrediction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDriverVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstDriverId = table.Column<int>(type: "int", nullable: false),
                    SecondDriverId = table.Column<int>(type: "int", nullable: false),
                    ThirdDriverId = table.Column<int>(type: "int", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDriverVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDriverVote_Drivers_FirstDriverId",
                        column: x => x.FirstDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDriverVote_Drivers_SecondDriverId",
                        column: x => x.SecondDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDriverVote_Drivers_ThirdDriverId",
                        column: x => x.ThirdDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDriverVote_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RacePrediction_RaceId",
                table: "RacePrediction",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RacePrediction_SecondId",
                table: "RacePrediction",
                column: "SecondId");

            migrationBuilder.CreateIndex(
                name: "IX_RacePrediction_ThirdId",
                table: "RacePrediction",
                column: "ThirdId");

            migrationBuilder.CreateIndex(
                name: "IX_RacePrediction_UserId",
                table: "RacePrediction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RacePrediction_WinnerId",
                table: "RacePrediction",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverVote_FirstDriverId",
                table: "UserDriverVote",
                column: "FirstDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverVote_SecondDriverId",
                table: "UserDriverVote",
                column: "SecondDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverVote_ThirdDriverId",
                table: "UserDriverVote",
                column: "ThirdDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDriverVote_UserId",
                table: "UserDriverVote",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RacePrediction");

            migrationBuilder.DropTable(
                name: "UserDriverVote");
        }
    }
}
