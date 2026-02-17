using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace f1api.Migrations
{
    /// <inheritdoc />
    public partial class AddImgUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamImageUrl",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RaceImageUrl",
                table: "Races",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamImageUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "RaceImageUrl",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drivers");
        }
    }
}
