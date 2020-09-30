using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Persistence.Migrations
{
	public partial class init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				"Topics",
				table => new
				{
					Id = table.Column<int>("INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Name = table.Column<string>("TEXT", nullable: false),
					AuditCreatedById = table.Column<string>("TEXT", nullable: false),
					AuditCreatedByName = table.Column<string>("TEXT", nullable: false),
					AuditCreatedDateUtc = table.Column<DateTime>("TEXT", nullable: false),
					AuditLastModifiedById = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedByName = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedUtc = table.Column<DateTime>("TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Topics", x => x.Id);
				});

			migrationBuilder.CreateTable(
				"Users",
				table => new
				{
					Id = table.Column<int>("INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					EmailAddress = table.Column<string>("TEXT", nullable: false),
					Password = table.Column<string>("TEXT", nullable: false),
					Username = table.Column<string>("TEXT", nullable: false),
					AuditCreatedById = table.Column<string>("TEXT", nullable: false),
					AuditCreatedByName = table.Column<string>("TEXT", nullable: false),
					AuditCreatedDateUtc = table.Column<DateTime>("TEXT", nullable: false),
					AuditLastModifiedById = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedByName = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedUtc = table.Column<DateTime>("TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				"Threads",
				table => new
				{
					Id = table.Column<int>("INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Title = table.Column<string>("TEXT", nullable: false),
					TopicId = table.Column<int>("INTEGER", nullable: true),
					AuditCreatedById = table.Column<string>("TEXT", nullable: false),
					AuditCreatedByName = table.Column<string>("TEXT", nullable: false),
					AuditCreatedDateUtc = table.Column<DateTime>("TEXT", nullable: false),
					AuditLastModifiedById = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedByName = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedUtc = table.Column<DateTime>("TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Threads", x => x.Id);
					table.ForeignKey(
						"FK_Threads_Topics_TopicId",
						x => x.TopicId,
						"Topics",
						"Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				"Posts",
				table => new
				{
					Id = table.Column<int>("INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					Content = table.Column<string>("TEXT", nullable: false),
					CreatedById = table.Column<int>("INTEGER", nullable: true),
					ThreadId = table.Column<int>("INTEGER", nullable: true),
					AuditCreatedById = table.Column<string>("TEXT", nullable: false),
					AuditCreatedByName = table.Column<string>("TEXT", nullable: false),
					AuditCreatedDateUtc = table.Column<DateTime>("TEXT", nullable: false),
					AuditLastModifiedById = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedByName = table.Column<string>("TEXT", nullable: true),
					AuditLastModifiedUtc = table.Column<DateTime>("TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Posts", x => x.Id);
					table.ForeignKey(
						"FK_Posts_Threads_ThreadId",
						x => x.ThreadId,
						"Threads",
						"Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						"FK_Posts_Users_CreatedById",
						x => x.CreatedById,
						"Users",
						"Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				"IX_Posts_CreatedById",
				"Posts",
				"CreatedById");

			migrationBuilder.CreateIndex(
				"IX_Posts_ThreadId",
				"Posts",
				"ThreadId");

			migrationBuilder.CreateIndex(
				"IX_Threads_TopicId",
				"Threads",
				"TopicId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"Posts");

			migrationBuilder.DropTable(
				"Threads");

			migrationBuilder.DropTable(
				"Users");

			migrationBuilder.DropTable(
				"Topics");
		}
	}
}
