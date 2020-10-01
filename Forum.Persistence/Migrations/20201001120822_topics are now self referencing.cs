using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Persistence.Migrations
{
    public partial class topicsarenowselfreferencing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Topics",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ParentId",
                table: "Topics",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Topics_ParentId",
                table: "Topics",
                column: "ParentId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Topics_ParentId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_ParentId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Topics");
        }
    }
}
