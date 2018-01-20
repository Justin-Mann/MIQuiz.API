using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MIQuizAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagesTbl",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageBLOB = table.Column<byte[]>(nullable: true),
                    ImageURI = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesTbl", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "UsersTbl",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Role = table.Column<int>(nullable: false, defaultValue: 0),
                    UserName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTbl", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VideosTbl",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    VideoURI = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideosTbl", x => x.VideoId);
                });

            migrationBuilder.CreateTable(
                name: "QuizTbl",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    GradingCriteria = table.Column<string>(maxLength: 500, nullable: true),
                    Instructions = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTbl", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_QuizTbl_UsersTbl_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnswerTbl",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: false),
                    VideoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerTbl", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_AnswerTbl_ImagesTbl_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImagesTbl",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerTbl_VideosTbl_VideoId",
                        column: x => x.VideoId,
                        principalTable: "VideosTbl",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTbl",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: false),
                    Type = table.Column<string>(nullable: false),
                    VideoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTbl", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_QuestionTbl_ImagesTbl_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImagesTbl",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionTbl_VideosTbl_VideoId",
                        column: x => x.VideoId,
                        principalTable: "VideosTbl",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerTbl",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    IsCorrectAnswer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswerTbl", x => new { x.AnswerId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionAnswerTbl_AnswerTbl_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "AnswerTbl",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerTbl_QuestionTbl_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTbl",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionTbl",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionTbl", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestionTbl_QuestionTbl_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTbl",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuizQuestionTbl_QuizTbl_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizTbl",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTbl_ImageId",
                table: "AnswerTbl",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTbl_VideoId",
                table: "AnswerTbl",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerTbl_QuestionId",
                table: "QuestionAnswerTbl",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTbl_ImageId",
                table: "QuestionTbl",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTbl_VideoId",
                table: "QuestionTbl",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionTbl_QuestionId",
                table: "QuizQuestionTbl",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTbl_UserId",
                table: "QuizTbl",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswerTbl");

            migrationBuilder.DropTable(
                name: "QuizQuestionTbl");

            migrationBuilder.DropTable(
                name: "AnswerTbl");

            migrationBuilder.DropTable(
                name: "QuestionTbl");

            migrationBuilder.DropTable(
                name: "QuizTbl");

            migrationBuilder.DropTable(
                name: "ImagesTbl");

            migrationBuilder.DropTable(
                name: "VideosTbl");

            migrationBuilder.DropTable(
                name: "UsersTbl");
        }
    }
}
