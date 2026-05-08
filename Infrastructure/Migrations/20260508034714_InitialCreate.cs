using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    FlagEmoji = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NativeLanguageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLanguage_Languages_NativeLanguageId",
                        column: x => x.NativeLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLanguage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserLanguageId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Term = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Translation = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Romanization = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    IsPhrase = table.Column<bool>(type: "INTEGER", nullable: false),
                    Source = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_UserLanguage_UserLanguageId",
                        column: x => x.UserLanguageId,
                        principalTable: "UserLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardExample",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Sentence = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Translation = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardExample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardExample_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Easiness = table.Column<double>(type: "REAL", nullable: false),
                    Interval = table.Column<int>(type: "INTEGER", nullable: false),
                    Repetitions = table.Column<int>(type: "INTEGER", nullable: false),
                    NextReview = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastReviewed = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardProgress_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardProgress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "CreatedAt", "FlagEmoji", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "pt", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇧🇷", "Português", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "en", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇺🇸", "Inglês", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "ja", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇯🇵", "Japonês", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "zh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇨🇳", "Chinês", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "es", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇪🇸", "Espanhol", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "fr", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇫🇷", "Francês", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "de", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇩🇪", "Alemão", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "ko", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇰🇷", "Coreano", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "it", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇮🇹", "Italiano", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "ru", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "🇷🇺", "Russo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_UserLanguageId",
                table: "Card",
                column: "UserLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CardExample_CardId",
                table: "CardExample",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardProgress_CardId",
                table: "CardProgress",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardProgress_UserId",
                table: "CardProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardProgress_UserId_NextReview",
                table: "CardProgress",
                columns: new[] { "UserId", "NextReview" });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguage_LanguageId",
                table: "UserLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguage_NativeLanguageId",
                table: "UserLanguage",
                column: "NativeLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguage_UserId_LanguageId",
                table: "UserLanguage",
                columns: new[] { "UserId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardExample");

            migrationBuilder.DropTable(
                name: "CardProgress");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "UserLanguage");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
