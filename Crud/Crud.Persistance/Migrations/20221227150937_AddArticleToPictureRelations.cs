using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud.Persistance.Migrations
{
    public partial class AddArticleToPictureRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ArticlePicture",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    PicturesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePicture", x => new { x.ArticlesId, x.PicturesId });
                    table.ForeignKey(
                        name: "FK_ArticlePicture_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePicture_Pictures_PicturesId",
                        column: x => x.PicturesId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PictureId",
                table: "Articles",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePicture_PicturesId",
                table: "ArticlePicture",
                column: "PicturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Pictures_PictureId",
                table: "Articles",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Pictures_PictureId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticlePicture");

            migrationBuilder.DropIndex(
                name: "IX_Articles_PictureId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Articles");
        }
    }
}
