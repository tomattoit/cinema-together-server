using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CityId", "DateOfBirth", "Email", "Gender", "Name", "PasswordHash", "ProfilePicturePath", "Rating", "RatingCount", "Role", "TwoFactorEnabled", "Username" },
                values: new object[,]
                {
                    { new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"), null, new DateTime(2000, 2, 27, 10, 13, 49, 949, DateTimeKind.Local).AddTicks(188), "d.krumkachev@gmail.com", 1, "Robby Krieger", "ACDDCFAB1CC3B1354428D848F7B004CF7D080ED9ABC8AFE7EF93478B399C0AEEDB34802F1D00B14FD67FDD4AEA0F6A2B", null, 0m, 0, 1, false, "Test1" },
                    { new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"), null, new DateTime(2002, 2, 27, 10, 13, 49, 954, DateTimeKind.Local).AddTicks(3622), "artemij1258@gmail.com", 1, "John Densmore", "12BDAF322041BC4D0481630C7E7FD602E9BD9FB8310D897B4344EB014B311096D5B0DDE1B63B126717DEF765DD0C3149", null, 0m, 0, 0, false, "Test2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
