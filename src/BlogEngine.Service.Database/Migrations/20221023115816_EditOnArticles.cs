using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.Service.Database.Migrations
{
    public partial class EditOnArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_articles_user_info_id",
                table: "articles");

            migrationBuilder.CreateIndex(
                name: "IX_articles_user_info_id",
                table: "articles",
                column: "user_info_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_articles_user_info_id",
                table: "articles");

            migrationBuilder.CreateIndex(
                name: "IX_articles_user_info_id",
                table: "articles",
                column: "user_info_id",
                unique: true);
        }
    }
}
