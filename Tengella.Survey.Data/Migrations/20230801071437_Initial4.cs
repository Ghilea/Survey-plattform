using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tengella.Survey.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateIdSent",
                table: "Distribution");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateIdSent",
                table: "Distribution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 1,
                column: "TemplateIdSent",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Distribution",
                keyColumn: "Id",
                keyValue: 2,
                column: "TemplateIdSent",
                value: 0);
        }
    }
}
