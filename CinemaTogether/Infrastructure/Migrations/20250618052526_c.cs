using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Groups",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 6, 18, 8, 25, 24, 747, DateTimeKind.Local).AddTicks(5229), "9FBD04FF93C1717849C179D1F169A2108F7D20AD2DA9C9B9E61A69AFEA84AAF61964E5429E95F90E310A718964340A6A" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 6, 18, 8, 25, 24, 753, DateTimeKind.Local).AddTicks(5764), "9220370DBEDCCED2D08F4CD1B4CB9E2187704384F05C6B0D3F4F96582E1FF6810954744CD027ECC24701012D848D80F4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Groups",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000,
                oldNullable: true);

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
    }
}
