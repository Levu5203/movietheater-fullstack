using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreColumnsAndMovieEnddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreId",
                schema: "Common",
                table: "Genres",
                newName: "Id");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                schema: "Common",
                table: "Movies",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "MovieGenres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "MovieGenres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Common",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "MovieGenres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "MovieGenres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "Genres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "Genres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Common",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "Genres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_CreatedById",
                schema: "Common",
                table: "MovieGenres",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_DeletedById",
                schema: "Common",
                table: "MovieGenres",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_UpdatedById",
                schema: "Common",
                table: "MovieGenres",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CreatedById",
                schema: "Common",
                table: "Genres",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_DeletedById",
                schema: "Common",
                table: "Genres",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_UpdatedById",
                schema: "Common",
                table: "Genres",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_CreatedById",
                schema: "Common",
                table: "Genres",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_DeletedById",
                schema: "Common",
                table: "Genres",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Users_UpdatedById",
                schema: "Common",
                table: "Genres",
                column: "UpdatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Users_CreatedById",
                schema: "Common",
                table: "MovieGenres",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Users_DeletedById",
                schema: "Common",
                table: "MovieGenres",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Users_UpdatedById",
                schema: "Common",
                table: "MovieGenres",
                column: "UpdatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_CreatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_DeletedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Users_UpdatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Users_CreatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Users_DeletedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Users_UpdatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_CreatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_DeletedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_UpdatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_CreatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_DeletedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_UpdatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "Common",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "Genres");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Common",
                table: "Genres",
                newName: "GenreId");
        }
    }
}
