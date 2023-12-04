﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using interval_recall.DAL.EF;

#nullable disable

namespace interval_recall.DAL.Migrations
{
    [DbContext(typeof(IntervaRecallContext))]
    [Migration("20231128093335_AddQuestionStepAndState")]
    partial class AddQuestionStepAndState
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("interval_recall.DAL.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.DecisionQuality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("DecisionQualities");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("EasyFactor")
                        .HasColumnType("REAL");

                    b.Property<int>("Interval")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("QuestionGroupId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RepetitionDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Repetitions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Step")
                        .HasColumnType("TEXT");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionGroupId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.QuestionGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("EasyBonus")
                        .HasColumnType("REAL");

                    b.Property<double>("IntervalModifier")
                        .HasColumnType("REAL");

                    b.Property<double>("NewInterval")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionGroups");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserGroupId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.Answer", b =>
                {
                    b.HasOne("interval_recall.DAL.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.DecisionQuality", b =>
                {
                    b.HasOne("interval_recall.DAL.Entities.Question", "Question")
                        .WithMany("DecisionQualities")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.Question", b =>
                {
                    b.HasOne("interval_recall.DAL.Entities.QuestionGroup", "QuestionGroup")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionGroup");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.QuestionGroup", b =>
                {
                    b.HasOne("interval_recall.DAL.Entities.User", "User")
                        .WithMany("QuestionGroups")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.User", b =>
                {
                    b.HasOne("interval_recall.DAL.Entities.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId");

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("DecisionQualities");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.QuestionGroup", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.User", b =>
                {
                    b.Navigation("QuestionGroups");
                });

            modelBuilder.Entity("interval_recall.DAL.Entities.UserGroup", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
