using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge.Data.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vehicle",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 112, 134, 54, 248, 243, 67, 232, 172, 135, 219, 254, 206, 161, 54, 28, 19, 51, 58, 79, 207, 21, 50, 234, 231, 128, 36, 205, 52, 115, 103, 193, 2, 102, 82, 25, 178, 208, 225, 66, 221, 127, 64, 152, 184, 54, 75, 250, 179, 156, 250, 122, 212, 148, 253, 209, 7, 47, 29, 3, 223, 92, 50, 207, 0 }, new byte[] { 44, 17, 11, 200, 43, 187, 46, 5, 121, 62, 184, 197, 13, 206, 217, 240, 220, 252, 149, 126, 15, 19, 105, 212, 221, 250, 91, 36, 238, 25, 56, 196, 68, 135, 69, 45, 164, 149, 59, 115, 142, 121, 67, 131, 149, 28, 111, 160, 15, 187, 111, 30, 214, 121, 211, 45, 180, 102, 140, 57, 171, 206, 134, 233, 40, 196, 131, 1, 254, 184, 148, 33, 226, 235, 26, 17, 183, 32, 150, 238, 133, 184, 82, 245, 213, 143, 15, 12, 11, 201, 108, 116, 198, 82, 24, 34, 163, 234, 106, 33, 235, 21, 24, 118, 155, 31, 60, 244, 239, 138, 96, 6, 192, 189, 245, 140, 39, 178, 151, 200, 33, 207, 8, 90, 96, 132, 124, 8 } });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2019, 2, 20, 17, 40, 52, 367, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2019, 2, 20, 17, 40, 52, 371, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2019, 2, 20, 17, 40, 52, 371, DateTimeKind.Local), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_UserId",
                table: "Vehicle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_User_UserId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_UserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 151, 128, 30, 216, 131, 214, 49, 128, 73, 82, 101, 3, 203, 209, 127, 171, 10, 15, 28, 155, 197, 205, 83, 182, 71, 208, 59, 89, 122, 103, 157, 145, 233, 98, 14, 75, 35, 10, 62, 229, 206, 2, 161, 1, 255, 2, 63, 154, 202, 221, 205, 132, 130, 250, 133, 16, 210, 98, 246, 62, 119, 163, 125, 37 }, new byte[] { 151, 210, 77, 157, 38, 190, 128, 172, 156, 86, 1, 76, 247, 131, 160, 14, 224, 16, 79, 232, 229, 117, 217, 151, 112, 249, 236, 213, 59, 133, 17, 80, 10, 100, 45, 185, 241, 211, 97, 55, 212, 165, 250, 199, 118, 86, 168, 56, 3, 194, 133, 136, 243, 119, 138, 121, 119, 248, 46, 184, 1, 41, 251, 134, 147, 106, 54, 51, 194, 214, 110, 26, 17, 237, 64, 250, 7, 137, 173, 212, 240, 102, 250, 27, 244, 62, 36, 69, 124, 248, 114, 195, 202, 130, 18, 237, 157, 63, 75, 1, 34, 171, 135, 218, 33, 243, 46, 241, 21, 138, 104, 175, 14, 33, 55, 106, 215, 61, 89, 159, 159, 56, 182, 7, 52, 255, 54, 162 } });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 14, 9, 21, 504, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 14, 9, 21, 509, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 14, 9, 21, 509, DateTimeKind.Local));
        }
    }
}
