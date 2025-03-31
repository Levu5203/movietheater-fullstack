using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixShowTimeSlotModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "ShowTimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes",
                column: "ShowTimeSlotId",
                principalSchema: "Common",
                principalTable: "ShowTimeSlots",
                principalColumn: "ShowTimeSlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_ShowTimeSlots_ShowTimeSlotId",
                schema: "Common",
                table: "ShowTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowTimeSlots",
                schema: "Common",
                table: "ShowTimeSlots",
                column: "Id");

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
        }
    }
}
