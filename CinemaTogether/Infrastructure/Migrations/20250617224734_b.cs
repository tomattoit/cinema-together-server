using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 6, 18, 1, 47, 32, 996, DateTimeKind.Local).AddTicks(5900), "BA3CB4B46047250C3F442B500D2C91050F73A84BCFA4343D2CDAD410A14E8EC34884AB8FC50A16B60F5531C187C19552" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 6, 18, 1, 47, 33, 3, DateTimeKind.Local).AddTicks(6946), "0F74D762C49969BDF03B3216110F352AF47C3DB5EAAB05ED9BDC5BEC4296DAC2160A6A389687351AF4D493DF1ED96C70" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 6, 18, 1, 44, 32, 593, DateTimeKind.Local).AddTicks(6549), "C40A95BE4F9395E279CBC1A25F16AAFD744123553644B5384E19A5436184E3EFDBECCA8E6063D1FD25DC3BE040B76754" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 6, 18, 1, 44, 32, 600, DateTimeKind.Local).AddTicks(6521), "49F658443B1B5D89AA164C7C2E58721217BAC79418305A8BFD8D2957E81D1842D33B9A22F48FA1D1F41CCC014005EBBA" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }
    }
}
