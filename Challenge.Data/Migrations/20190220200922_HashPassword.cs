using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge.Data.Migrations
{
    public partial class HashPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "User",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "vL/VRC9EEUgyaB6XwFwLZ68C7TQYPwPeat/NiMEvqFw=");

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 13, 31, 19, 393, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 13, 31, 19, 402, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "VehicleId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 2, 20, 13, 31, 19, 402, DateTimeKind.Local));
        }
    }
}
