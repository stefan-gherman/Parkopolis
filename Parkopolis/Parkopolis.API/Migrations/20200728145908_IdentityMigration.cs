using Microsoft.EntityFrameworkCore.Migrations;

namespace Parkopolis.API.Migrations
{
    public partial class IdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLot_User_UserId",
                table: "ParkingLot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLot_UserId",
                table: "ParkingLot");

            migrationBuilder.AddColumn<int>(
                name: "UserViewModelId",
                table: "ParkingLot",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLot_UserViewModelId",
                table: "ParkingLot",
                column: "UserViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLot_User_UserViewModelId",
                table: "ParkingLot",
                column: "UserViewModelId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLot_User_UserViewModelId",
                table: "ParkingLot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLot_UserViewModelId",
                table: "ParkingLot");

            migrationBuilder.DropColumn(
                name: "UserViewModelId",
                table: "ParkingLot");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLot_UserId",
                table: "ParkingLot",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLot_User_UserId",
                table: "ParkingLot",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
