using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "IsEmailVerified", "PasswordHash" },
                values: new object[] { new DateTime(2000, 3, 12, 15, 34, 42, 977, DateTimeKind.Local).AddTicks(2734), true, "19EBCDBC1E08AF380BCDC9713CE4D837E3EADD0D9E84932C61C9FD684FE5CD62228BB340DA483B4C6EA227CFEA7F955C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "IsEmailVerified", "PasswordHash" },
                values: new object[] { new DateTime(2002, 3, 12, 15, 34, 42, 982, DateTimeKind.Local).AddTicks(5811), true, "C8CE79ECF4A4D6EE9A84FA410913FB878F9E8F3920E44568EDA5F0B693805A1B270304E524B3EFDCA8B03675466C778A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "IsEmailVerified", "PasswordHash" },
                values: new object[] { new DateTime(2000, 3, 12, 14, 57, 41, 190, DateTimeKind.Local).AddTicks(6474), false, "13112ADB80ADA706BAB637B7A02E1D2D9F85BF2D53B7F6CF5AF58420A93E1D96296BF52339779755CB80BF12F0048272" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "IsEmailVerified", "PasswordHash" },
                values: new object[] { new DateTime(2002, 3, 12, 14, 57, 41, 195, DateTimeKind.Local).AddTicks(9373), false, "5B5C8CA0ED37040D713DF4D57F89526A63FD8F5823E257886C28A097082648893F050302671ECE312BE457EF71C3C453" });
        }
    }
}
