﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MIQuizAPI.Database.Context;
using System;

namespace MIQuizAPI.Migrations
{
    [DbContext(typeof(MIQuizContext))]
    [Migration("20180110211751_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MIQuizAPI.Database.Models.AnswerDef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsCorrectAnswer");

                    b.Property<int?>("Order");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswerTbl");
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.QuestionDef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ConsumerQuizId");

                    b.Property<byte[]>("ImageBlob");

                    b.Property<string>("ImageURI");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("Order");

                    b.Property<string>("Text")
                        .HasMaxLength(250);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<string>("videoURI");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerQuizId");

                    b.ToTable("QuestionTbl");
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.QuizDef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("GradingCriteria")
                        .HasMaxLength(500);

                    b.Property<string>("Instructions")
                        .HasMaxLength(500);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("Order");

                    b.Property<int>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("QuizTbl");
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(20);

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName")
                        .HasMaxLength(20);

                    b.Property<int>("Role");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("UsersTbl");
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.AnswerDef", b =>
                {
                    b.HasOne("MIQuizAPI.Database.Models.QuestionDef", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.QuestionDef", b =>
                {
                    b.HasOne("MIQuizAPI.Database.Models.QuizDef", "ConsumerQuiz")
                        .WithMany("Questions")
                        .HasForeignKey("ConsumerQuizId");
                });

            modelBuilder.Entity("MIQuizAPI.Database.Models.QuizDef", b =>
                {
                    b.HasOne("MIQuizAPI.Database.Models.User", "Owner")
                        .WithMany("CreatedQuizes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
