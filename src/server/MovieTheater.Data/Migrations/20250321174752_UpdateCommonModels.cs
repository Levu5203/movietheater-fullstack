using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommonModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryScores",
                schema: "Common",
                table: "HistoryScores");

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
                table: "ShowTimeSlots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "ShowTimeSlots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots",
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

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Common",
                table: "HistoryScores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Common",
                table: "HistoryScores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                schema: "Common",
                table: "HistoryScores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "Common",
                table: "HistoryScores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                schema: "Common",
                table: "HistoryScores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Common",
                table: "HistoryScores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Common",
                table: "HistoryScores",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                schema: "Common",
                table: "HistoryScores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryScores",
                schema: "Common",
                table: "HistoryScores",
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
                name: "IX_ShowTimeSlots_CreatedById",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimeSlots_DeletedById",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimeSlots_UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots",
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

            migrationBuilder.CreateIndex(
                name: "IX_HistoryScores_CreatedById",
                schema: "Common",
                table: "HistoryScores",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryScores_DeletedById",
                schema: "Common",
                table: "HistoryScores",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryScores_UpdatedById",
                schema: "Common",
                table: "HistoryScores",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryScores_Users_CreatedById",
                schema: "Common",
                table: "HistoryScores",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryScores_Users_DeletedById",
                schema: "Common",
                table: "HistoryScores",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryScores_Users_UpdatedById",
                schema: "Common",
                table: "HistoryScores",
                column: "UpdatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

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
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes",
                column: "ShowTimeSlotId",
                principalSchema: "Common",
                principalTable: "ShowTimeSlots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimeSlots_Users_CreatedById",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "CreatedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimeSlots_Users_DeletedById",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "DeletedById",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimeSlots_Users_UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryScores_Users_CreatedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryScores_Users_DeletedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryScores_Users_UpdatedById",
                schema: "Common",
                table: "HistoryScores");

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
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimeSlots_Users_CreatedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimeSlots_Users_DeletedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimeSlots_Users_UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots");

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
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimeSlots_CreatedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimeSlots_DeletedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimeSlots_UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryScores",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropIndex(
                name: "IX_HistoryScores_CreatedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropIndex(
                name: "IX_HistoryScores_DeletedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropIndex(
                name: "IX_HistoryScores_UpdatedById",
                schema: "Common",
                table: "HistoryScores");

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
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "ShowTimeSlots");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "ShowTimeSlots");

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
                name: "Id",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                schema: "Common",
                table: "HistoryScores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketShowTimeMovies",
                schema: "Common",
                table: "TicketShowTimeMovies",
                column: "TicketShowTimeMovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "ShowTimeSlotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "SeatShowTimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryScores",
                schema: "Common",
                table: "HistoryScores",
                column: "HistoryScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes",
                column: "ShowTimeSlotId",
                principalSchema: "Common",
                principalTable: "ShowTimeSlots",
                principalColumn: "ShowTimeSlotId");
        }
    }
}
