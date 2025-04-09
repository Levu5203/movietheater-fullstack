using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketInvoiceModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Common",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaRoomId",
                schema: "Common",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                schema: "Common",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShowTimeId",
                schema: "Common",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalMoney",
                schema: "Common",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaRoomId",
                schema: "Common",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                schema: "Common",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "TicketIssued",
                schema: "Common",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CinemaRoomId",
                schema: "Common",
                table: "Tickets",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MovieId",
                schema: "Common",
                table: "Tickets",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowTimeId",
                schema: "Common",
                table: "Tickets",
                column: "ShowTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CinemaRoomId",
                schema: "Common",
                table: "Invoices",
                column: "CinemaRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MovieId",
                schema: "Common",
                table: "Invoices",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_CinemaRooms_CinemaRoomId",
                schema: "Common",
                table: "Invoices",
                column: "CinemaRoomId",
                principalSchema: "Common",
                principalTable: "CinemaRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Movies_MovieId",
                schema: "Common",
                table: "Invoices",
                column: "MovieId",
                principalSchema: "Common",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CinemaRooms_CinemaRoomId",
                schema: "Common",
                table: "Tickets",
                column: "CinemaRoomId",
                principalSchema: "Common",
                principalTable: "CinemaRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                schema: "Common",
                table: "Tickets",
                column: "MovieId",
                principalSchema: "Common",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ShowTimes_ShowTimeId",
                schema: "Common",
                table: "Tickets",
                column: "ShowTimeId",
                principalSchema: "Common",
                principalTable: "ShowTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_CinemaRooms_CinemaRoomId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Movies_MovieId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CinemaRooms_CinemaRoomId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Movies_MovieId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ShowTimes_ShowTimeId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CinemaRoomId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MovieId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ShowTimeId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CinemaRoomId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_MovieId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MovieId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                schema: "Common",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CinemaRoomId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "MovieId",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TicketIssued",
                schema: "Common",
                table: "Invoices");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                schema: "Common",
                table: "Tickets",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalMoney",
                schema: "Common",
                table: "Invoices",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Common",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
