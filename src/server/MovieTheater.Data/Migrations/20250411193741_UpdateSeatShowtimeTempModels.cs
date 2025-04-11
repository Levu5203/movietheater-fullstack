using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeatShowtimeTempModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "Id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatShowTimes",
                schema: "Common",
                table: "SeatShowTimes",
                column: "SeatShowTimeId");
        }
    }
}
