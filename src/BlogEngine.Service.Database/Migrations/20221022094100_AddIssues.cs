using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlogEngine.Service.Database.Migrations
{
    /// <summary>
    ///     Добавляет выпуски журнала
    ///     и тэги для статей
    /// </summary>
    public partial class AddIssues : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "issue_id",
                table: "articles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string[]>(
                name: "tags",
                table: "articles",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateTable(
                name: "issues",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_number = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    leading_image_id = table.Column<long>(type: "bigint", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issues", x => x.id);
                    table.ForeignKey(
                        name: "FK_issues_image_strings_leading_image_id",
                        column: x => x.leading_image_id,
                        principalTable: "image_strings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_issue_id",
                table: "articles",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "IX_issues_issue_number",
                table: "issues",
                column: "issue_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_issues_leading_image_id",
                table: "issues",
                column: "leading_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_issues_article",
                table: "articles",
                column: "issue_id",
                principalTable: "issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_issues_article",
                table: "articles");

            migrationBuilder.DropTable(
                name: "issues");

            migrationBuilder.DropIndex(
                name: "IX_articles_issue_id",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "issue_id",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "tags",
                table: "articles");
        }
    }
}
