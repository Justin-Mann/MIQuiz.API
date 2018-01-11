using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MIQuizAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    Role = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    GradingCriteria = table.Column<string>(maxLength: 500, nullable: true),
                    Instructions = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Order = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizTbl_UsersTbl_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "UsersTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsumerQuizId = table.Column<int>(nullable: true),
                    ImageBlob = table.Column<byte[]>(nullable: true),
                    ImageURI = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    Text = table.Column<string>(maxLength: 250, nullable: true),
                    Type = table.Column<string>(nullable: false),
                    videoURI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTbl_QuizTbl_ConsumerQuizId",
                        column: x => x.ConsumerQuizId,
                        principalTable: "QuizTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerTbl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCorrectAnswer = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerTbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerTbl_QuestionTbl_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTbl_QuestionId",
                table: "AnswerTbl",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTbl_ConsumerQuizId",
                table: "QuestionTbl",
                column: "ConsumerQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTbl_OwnerId",
                table: "QuizTbl",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerTbl");

            migrationBuilder.DropTable(
                name: "QuestionTbl");

            migrationBuilder.DropTable(
                name: "QuizTbl");

            migrationBuilder.DropTable(
                name: "UsersTbl");
        }
    }
}
