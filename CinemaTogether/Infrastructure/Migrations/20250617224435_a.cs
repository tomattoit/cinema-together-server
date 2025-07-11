using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "IsDeleted", "PasswordHash" },
                values: new object[] { new DateTime(2000, 6, 18, 1, 44, 32, 593, DateTimeKind.Local).AddTicks(6549), false, "C40A95BE4F9395E279CBC1A25F16AAFD744123553644B5384E19A5436184E3EFDBECCA8E6063D1FD25DC3BE040B76754" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "IsDeleted", "PasswordHash" },
                values: new object[] { new DateTime(2002, 6, 18, 1, 44, 32, 600, DateTimeKind.Local).AddTicks(6521), false, "49F658443B1B5D89AA164C7C2E58721217BAC79418305A8BFD8D2957E81D1842D33B9A22F48FA1D1F41CCC014005EBBA" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 5, 17, 18, 53, 7, 92, DateTimeKind.Local).AddTicks(5229), "A4602C57CE0501C34685931CE0183C48B8CB07BBEE9708E586CCDF8374004C339BDD57692D778145D84E95DDB928EF04" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 5, 17, 18, 53, 7, 97, DateTimeKind.Local).AddTicks(9450), "026A2131A4ECC8654681156754ACC9C6EA6D237423F475EEEA3897B17F36063EAEE11FAF2CEB11B1C6087E6D2445695B" });
        }
    }
}
