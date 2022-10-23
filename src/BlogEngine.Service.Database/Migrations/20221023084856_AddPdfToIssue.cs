using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.Service.Database.Migrations
{
    /// <summary>
    ///     Добавление пдф в выпуски
    /// </summary>
    public partial class AddPdfToIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<string>(
                name: "pdf",
                table: "issues",
                type: "text",
                nullable: false,
                defaultValue: "");

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn(
                name: "pdf",
                table: "issues");
    }
}
