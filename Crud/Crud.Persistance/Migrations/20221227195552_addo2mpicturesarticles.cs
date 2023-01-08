using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.Persistance.Migrations
{
    public partial class addo2mpicturesarticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ThumbnailId",
                table: "Articles",
                column: "ThumbnailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Pictures_ThumbnailId",
                table: "Articles",
                column: "ThumbnailId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Pictures_ThumbnailId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ThumbnailId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Articles");
        }
    }
}
