using Microsoft.EntityFrameworkCore.Migrations;

namespace Parkopolis.API.Migrations
{
    public partial class ApplicationUserIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ParkingLot");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ParkingLot",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLot_ApplicationUserId",
                table: "ParkingLot",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLot_AspNetUsers_ApplicationUserId",
                table: "ParkingLot",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLot_AspNetUsers_ApplicationUserId",
                table: "ParkingLot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLot_ApplicationUserId",
                table: "ParkingLot");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ParkingLot");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ParkingLot",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
