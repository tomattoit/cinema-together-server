using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Actors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    RatingCount = table.Column<int>(type: "int", nullable: false),
                    RatingTmdb = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "ApiId", "Name" },
                values: new object[,]
                {
                    { new Guid("048291de-5df2-43cb-9b59-ddcc7ba2f3d4"), 28, "Action" },
                    { new Guid("0bd2dcfd-0cfa-4449-929f-748c399c8dbf"), 36, "History" },
                    { new Guid("1b33dcf2-e1ad-4a02-bd6a-9b874d6369cd"), 12, "Adventure" },
                    { new Guid("1cb76074-a2e9-4e50-bb64-165c93533598"), 10749, "Romance" },
                    { new Guid("27f6e407-e3d5-4b34-a713-a4da1a661604"), 16, "Animation" },
                    { new Guid("2ac7342a-f220-479e-9a7b-18afada9fbbe"), 14, "Fantasy" },
                    { new Guid("3489587e-6fa3-4790-8216-be5bbb3532bc"), 27, "Horror" },
                    { new Guid("3af9901c-a0cc-44c4-928b-e478617f9ff1"), 878, "Science Fiction" },
                    { new Guid("44d1426b-e51b-4605-ada6-5eb5d868c357"), 10751, "Family" },
                    { new Guid("45998a9a-0a9c-4986-9e0f-05faeb3b5932"), 10770, "TV Movie" },
                    { new Guid("881dbfa8-5503-47cb-9545-dd2ebc94e850"), 99, "Documentary" },
                    { new Guid("99116751-059a-4c22-8981-7479b7fb0e0e"), 80, "Crime" },
                    { new Guid("a4cd390f-39fd-462d-9b9e-7ff3640f7135"), 10402, "Music" },
                    { new Guid("ad48a3ca-bf42-4dcf-896a-d2a0934297be"), 18, "Drama" },
                    { new Guid("b504bd64-5df9-4546-b7e4-ffad637318be"), 10752, "War" },
                    { new Guid("ba415ac9-bc10-4652-99a1-fbf6e92beb4b"), 37, "Western" },
                    { new Guid("c3007078-1f8c-4f34-9986-0be2cb6e6306"), 9648, "Mystery" },
                    { new Guid("cd4ab5ce-61e7-4c0e-8cb1-c527250375ca"), 35, "Comedy" },
                    { new Guid("f015ce95-992a-4197-b86c-de5599ebbc98"), 53, "Thriller" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Id",
                table: "Movies",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a0440a78-41cc-419c-b05f-b511ee65d28a"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2000, 3, 12, 15, 34, 42, 977, DateTimeKind.Local).AddTicks(2734), "19EBCDBC1E08AF380BCDC9713CE4D837E3EADD0D9E84932C61C9FD684FE5CD62228BB340DA483B4C6EA227CFEA7F955C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ddc7d332-e194-4e6e-a77d-c1ebce29e746"),
                columns: new[] { "DateOfBirth", "PasswordHash" },
                values: new object[] { new DateTime(2002, 3, 12, 15, 34, 42, 982, DateTimeKind.Local).AddTicks(5811), "C8CE79ECF4A4D6EE9A84FA410913FB878F9E8F3920E44568EDA5F0B693805A1B270304E524B3EFDCA8B03675466C778A" });
        }
    }
}
