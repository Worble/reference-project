using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditCreatedById = table.Column<string>(nullable: false),
                    AuditCreatedByName = table.Column<string>(nullable: false),
                    AuditCreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AuditLastModifiedById = table.Column<string>(nullable: true),
                    AuditLastModifiedByName = table.Column<string>(nullable: true),
                    AuditLastModifiedUtc = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Topics_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditCreatedById = table.Column<string>(nullable: false),
                    AuditCreatedByName = table.Column<string>(nullable: false),
                    AuditCreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AuditLastModifiedById = table.Column<string>(nullable: true),
                    AuditLastModifiedByName = table.Column<string>(nullable: true),
                    AuditLastModifiedUtc = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditCreatedById = table.Column<string>(nullable: false),
                    AuditCreatedByName = table.Column<string>(nullable: false),
                    AuditCreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AuditLastModifiedById = table.Column<string>(nullable: true),
                    AuditLastModifiedByName = table.Column<string>(nullable: true),
                    AuditLastModifiedUtc = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threads_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuditCreatedById = table.Column<string>(nullable: false),
                    AuditCreatedByName = table.Column<string>(nullable: false),
                    AuditCreatedDateUtc = table.Column<DateTime>(nullable: false),
                    AuditLastModifiedById = table.Column<string>(nullable: true),
                    AuditLastModifiedByName = table.Column<string>(nullable: true),
                    AuditLastModifiedUtc = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    ThreadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_TopicId",
                table: "Threads",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ParentId",
                table: "Topics",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
