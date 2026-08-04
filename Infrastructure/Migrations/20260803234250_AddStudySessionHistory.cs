using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudySessionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CardsReviewed = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IncorrectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudySessionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudySessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    LevelBefore = table.Column<int>(type: "INTEGER", nullable: false),
                    LevelAfter = table.Column<int>(type: "INTEGER", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySessionAnswers_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySessionAnswers_StudySessions_StudySessionId",
                        column: x => x.StudySessionId,
                        principalTable: "StudySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySessionAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessionAnswers_CardId",
                table: "StudySessionAnswers",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessionAnswers_StudySessionId",
                table: "StudySessionAnswers",
                column: "StudySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessionAnswers_UserId",
                table: "StudySessionAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_UserId",
                table: "StudySessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_UserId_StartedAt",
                table: "StudySessions",
                columns: new[] { "UserId", "StartedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessionAnswers");

            migrationBuilder.DropTable(
                name: "StudySessions");
        }
    }
}
