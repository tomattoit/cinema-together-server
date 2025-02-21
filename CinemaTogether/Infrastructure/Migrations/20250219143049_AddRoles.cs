using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CityId", "DateOfBirth", "Email", "Gender", "Name", "PasswordHash", "ProfilePicturePath", "Rating", "RatingCount", "RoleId", "TwoFactorEnabled", "Username" },
                values: new object[,]
                {
                    { new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"), null, new DateTime(2000, 2, 19, 15, 30, 49, 621, DateTimeKind.Local).AddTicks(9016), "d.krumkachev@gmail.com", 1, "Robby Krieger", "BF7649FE8946D33D3F323551C145481A00442EF7DFD69AD7A4992FA87A8D9F25DF6325F5F385ED2FB12E94939DE91F3D", null, 0m, 0, new Guid("22222222-2222-2222-2222-222222222222"), false, "Test1" },
                    { new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"), null, new DateTime(2002, 2, 19, 15, 30, 49, 627, DateTimeKind.Local).AddTicks(1052), "artemij1258@gmail.com", 1, "John Densmore", "9A4CEE67F9C5ECB70B404FF40ED486D04A4EB08DAB7CA1278429A652F181B250011B922C2D642C2E8154BE45F2966AC8", null, 0m, 0, new Guid("11111111-1111-1111-1111-111111111111"), false, "Test2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
