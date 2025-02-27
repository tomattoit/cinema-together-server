using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIso2Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Iso2",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 2, 27, 11, 48, 16, 599, DateTimeKind.Local).AddTicks(2921), "6376B38BE5F2F0A44888FE9A8DD109A889B6CDFD6BDF5201E15EA5FDDA6A5D42D04FF5EDA47CE94F214B35812C57B19A" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 2, 27, 11, 48, 16, 604, DateTimeKind.Local).AddTicks(5599), "71E6A697FDD0AE35E7BAF6F8F406C6E9393992C9A174DDCC6E4183C360A610E5ABC403FE8A4FDCF07E8C74DCDBA6A584" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iso2",
                table: "Countries");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 2, 27, 10, 13, 49, 949, DateTimeKind.Local).AddTicks(188), "ACDDCFAB1CC3B1354428D848F7B004CF7D080ED9ABC8AFE7EF93478B399C0AEEDB34802F1D00B14FD67FDD4AEA0F6A2B" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 2, 27, 10, 13, 49, 954, DateTimeKind.Local).AddTicks(3622), "12BDAF322041BC4D0481630C7E7FD602E9BD9FB8310D897B4344EB014B311096D5B0DDE1B63B126717DEF765DD0C3149" });
        }
    }
}
