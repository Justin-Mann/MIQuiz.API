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
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    Role = table.Column<int>(nullable: false),
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
                    ImageURI = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
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
                    Order = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerTbl",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerImageImageId = table.Column<int>(nullable: true),
                    AnswerVideoVideoId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerTbl", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_AnswerTbl_ImagesTbl_AnswerImageImageId",
                        column: x => x.AnswerImageImageId,
                        principalTable: "ImagesTbl",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerTbl_VideosTbl_AnswerVideoVideoId",
                        column: x => x.AnswerVideoVideoId,
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
                    IsActive = table.Column<bool>(nullable: false),
                    QuestionImageImageId = table.Column<int>(nullable: true),
                    QuestionVideoVideoId = table.Column<int>(nullable: true),
                    Text = table.Column<string>(maxLength: 250, nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTbl", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_QuestionTbl_ImagesTbl_QuestionImageImageId",
                        column: x => x.QuestionImageImageId,
                        principalTable: "ImagesTbl",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionTbl_VideosTbl_QuestionVideoVideoId",
                        column: x => x.QuestionVideoVideoId,
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
                    AnswerOrder = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerTbl_QuestionTbl_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTbl",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionTbl",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestionOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionTbl", x => new { x.QuizId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizQuestionTbl_QuestionTbl_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionTbl",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestionTbl_QuizTbl_QuizId",
                        column: x => x.QuizId,
                        principalTable: "QuizTbl",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTbl_AnswerImageImageId",
                table: "AnswerTbl",
                column: "AnswerImageImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerTbl_AnswerVideoVideoId",
                table: "AnswerTbl",
                column: "AnswerVideoVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerTbl_QuestionId",
                table: "QuestionAnswerTbl",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTbl_QuestionImageImageId",
                table: "QuestionTbl",
                column: "QuestionImageImageId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTbl_QuestionVideoVideoId",
                table: "QuestionTbl",
                column: "QuestionVideoVideoId");

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
