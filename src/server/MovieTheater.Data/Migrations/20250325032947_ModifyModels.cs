using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatShowTimes_Users_CreatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatShowTimes_Users_DeletedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatShowTimes_Users_UpdatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketShowTimeMovies_Users_CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketShowTimeMovies_Users_DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketShowTimeMovies_Users_UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropIndex(
                name: "IX_TicketShowTimeMovies_CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropIndex(
                name: "IX_TicketShowTimeMovies_DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropIndex(
                name: "IX_TicketShowTimeMovies_UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_SeatShowTimes_CreatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_SeatShowTimes_DeletedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_SeatShowTimes_UpdatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropColumn(
                name: "HistoryScoreId",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                schema: "Common",
                table: "Promotions",
                type: "decimal(3,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "TicketShowTimeMovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "SeatShowTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Common",
                table: "SeatShowTimes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "SeatShowTimes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "SeatShowTimes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "SeatShowTimes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "SeatShowTimes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "SeatShowTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "SeatShowTimes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "SeatShowTimes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                schema: "Common",
                table: "Promotions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "HistoryScoreId",
                schema: "Common",
                table: "HistoryScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketShowTimeMovies_CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketShowTimeMovies_DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketShowTimeMovies_UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SeatShowTimes_CreatedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SeatShowTimes_DeletedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_SeatShowTimes_UpdatedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatShowTimes_Users_CreatedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatShowTimes_Users_DeletedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatShowTimes_Users_UpdatedById",
                schema: "Common",
                table: "SeatShowTimes",
                column: "UpdatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketShowTimeMovies_Users_CreatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketShowTimeMovies_Users_DeletedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketShowTimeMovies_Users_UpdatedById",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "UpdatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
