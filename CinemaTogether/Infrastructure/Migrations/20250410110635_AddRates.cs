using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieUserRates",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUserRates", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MovieUserRates_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUserRates_Movies_MovieId1",
                        column: x => x.MovieId1,
                        principalTable: "Movies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovieUserRates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUserRates_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 4, 10, 13, 6, 34, 461, DateTimeKind.Local).AddTicks(740), "CE2793EAB65C92EAAA3E7CC0C9CE670CAE6475E6524BD0A0B17864D5A8469315FD877C0D9D206996E56E025236792F88" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 4, 10, 13, 6, 34, 466, DateTimeKind.Local).AddTicks(4043), "01C4CDFAE39C72DB61DD80F1191E992D2EC39DF4732180E23ED3AFD753FA42CD9B15541EB00603827E84EE3D11B03279" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUserRates_MovieId1",
                table: "MovieUserRates",
                column: "MovieId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUserRates_UserId",
                table: "MovieUserRates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUserRates_UserId1",
                table: "MovieUserRates",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUserRates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 4, 2, 10, 38, 41, 804, DateTimeKind.Local).AddTicks(5985), "0EAAFEA8A7EF68A46FA34890E777E0D14B17AD1DE643DD4E22C74B3B4CBA088A29C6822BE0555338D10217634A2A6B7C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 4, 2, 10, 38, 41, 811, DateTimeKind.Local).AddTicks(9036), "FB4146EC097E7219BA76AA0EC273BD9D7EA46426CAA44ACDE09DF7200427A1BA7048B272C8FA3599BB31A615F42A9F80" });
        }
    }
}
