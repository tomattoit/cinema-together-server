using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailVerificationToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EmailVerificationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailVerificationTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserId",
                table: "EmailVerificationTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerificationTokens");

            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 3, 6, 14, 57, 45, 51, DateTimeKind.Local).AddTicks(4108), "41DD399F6B0831A7532BE0F0CA728085E840B410661D2794C818FD5D939E888E49736BC909F41B96729087976E64EA03" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 3, 6, 14, 57, 45, 56, DateTimeKind.Local).AddTicks(7359), "B758F437E638BCE7A184E4C97C80AF7913C6BA06281765EFB58F4CFE05A4EC9E01D3B6975F8D9E6BEBDBC1AAFE92B3CB" });
        }
    }
}
