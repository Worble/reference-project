﻿// <auto-generated />
using System;
using Forum.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Forum.Persistence.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20201001120822_topics are now self referencing")]
    partial class topicsarenowselfreferencing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("Forum.Application.Common.Posts.PostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditCreatedById")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditCreatedByName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AuditCreatedDateUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedByName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AuditLastModifiedUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ThreadId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ThreadId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Forum.Application.Common.Threads.ThreadEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditCreatedById")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditCreatedByName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AuditCreatedDateUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedByName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AuditLastModifiedUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TopicId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Forum.Application.Common.Topics.TopicEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditCreatedById")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditCreatedByName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AuditCreatedDateUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedByName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AuditLastModifiedUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Forum.Application.Common.Users.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditCreatedById")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditCreatedByName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AuditCreatedDateUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuditLastModifiedByName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("AuditLastModifiedUtc")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Forum.Application.Common.Posts.PostEntity", b =>
                {
                    b.HasOne("Forum.Application.Common.Users.UserEntity", "CreatedBy")
                        .WithMany("Posts")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Forum.Application.Common.Threads.ThreadEntity", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("Forum.Application.Common.Threads.ThreadEntity", b =>
                {
                    b.HasOne("Forum.Application.Common.Topics.TopicEntity", "Topic")
                        .WithMany("Threads")
                        .HasForeignKey("TopicId");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Forum.Application.Common.Topics.TopicEntity", b =>
                {
                    b.HasOne("Forum.Application.Common.Topics.TopicEntity", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Forum.Application.Common.Threads.ThreadEntity", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Forum.Application.Common.Topics.TopicEntity", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Threads");
                });

            modelBuilder.Entity("Forum.Application.Common.Users.UserEntity", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
