using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptipn",
                schema: "Common",
                table: "Movies",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "Common",
                table: "Movies",
                newName: "Descriptipn");
        }
    }
}
